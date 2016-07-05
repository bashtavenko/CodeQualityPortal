using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public class SummaryRepository : ISummaryRepository
    {
        private readonly CodeQualityContext _context;
        private bool _disposed;

        public SummaryRepository() 
            : this (new CodeQualityCreateDatabaseIfNotExists())
        {
        }

        public SummaryRepository(IDatabaseInitializer<CodeQualityContext> context)
        {
            _context = new CodeQualityContext(context);
        }

        public IList<MemberSummary> GetWorst(DateTime dateFrom, DateTime dateTo, int topX)
        {
            var result = _context.Metrics
                .Where(w => w.Branch == null && w.Date.Date >= dateFrom && w.Date.Date <= dateTo && w.Member != null)
                .GroupBy(g => new { Module = g.Module.Name, Namespace = g.Namespace.Name, Type = g.Type.Name, g.MemberId, Member = g.Member.Name })
                .Select(s => new MemberSummary
                {
                    Module = s.Key.Module,
                    Namespace = s.Key.Namespace,
                    Type = s.Key.Type,
                    Id = s.Key.MemberId.Value,
                    Name = s.Key.Member,
                    MaintainabilityIndex = s.Min(d => d.MaintainabilityIndex),
                    CyclomaticComplexity = s.Max(d => d.CyclomaticComplexity),
                    LinesOfCode = s.Max(d => d.LinesOfCode),
                    ClassCoupling = s.Max(d => d.ClassCoupling),
                    CodeCoverage = s.Max(d => d.CodeCoverage)
                })
                .OrderBy(o => o.MaintainabilityIndex)
                .Take(topX)
                .ToList();

            return result;
        }

        public KeyStats GetLatestKeyStats()
        {
            var lastRunDate = _context
                .Metrics
                .Where (w => w.Branch == null)
                .Select(s => s.Date)
                .OrderByDescending(s => s.Date)
                .FirstOrDefault();

            if (lastRunDate == null)
            {
                return null;
            }
                
            var modules = _context.Metrics
                .Where(w => w.BranchId == null && w.DateId == lastRunDate.DateId && w.ModuleId != null && w.NamespaceId == null && w.TypeId == null && w.MemberId == null)
                .Select(s => s)
                .ToList();

            var stats = new KeyStats(lastRunDate.Date, modules);
            return stats;
        }

        public IList<ViewModels.SystemStats> GetLatestSystemStats()
        {
            var systems = _context.Systems.Select(s => new SystemStats { DimSystem = s }).ToList();
            foreach (var system in systems)
            {
                var allDataPoints = _context.Metrics
                    .Where(m => m.BranchId == null && m.ModuleId != null && m.NamespaceId == null && m.TypeId == null && m.MemberId == null &&
                                m.Module.Systems.Any(s => s.SystemId == system.DimSystem.SystemId))
                    .Select(
                        s =>
                            new
                            {
                                Date = s.Date.Date,
                                MaintanabilityIndex = s.MaintainabilityIndex,
                                CodeCoverage = s.CodeCoverage ?? 0,
                                LinesOfCode = s.LinesOfCode
                            })
                    .GroupBy(g => g.Date) // System usually has more than one module
                    .OrderByDescending(o => o.Key)
                    .Take(10)
                    .ToList();

                allDataPoints = allDataPoints.OrderBy(s => s.Key).ToList();

                system.MaintainabilityIndex = new Trend(allDataPoints.Select(s => new DataPoint {Date = s.Key, Value = Convert.ToInt32(s.Average(d => d.MaintanabilityIndex))}).ToList());
                system.MaintainabilityIndex.CalculateSlope();

                system.LinesOfCode = new Trend(allDataPoints.Select(s => new DataPoint { Date = s.Key, Value = Convert.ToInt32(s.Sum(d => d.LinesOfCode))}).ToList());
                system.LinesOfCode.CalculateSlope();
                
                system.CodeCoverage = new Trend(allDataPoints.Select(s => new DataPoint {Date = s.Key, Value = Convert.ToInt32(s.Average(d => d.CodeCoverage))}).ToList());
                system.CodeCoverage.CalculateSlope();
                
            }
            var items = Mapper.Map<List<ViewModels.SystemStats>>(systems);
            return items;
        }

        public IList<ViewModels.DataPoint> GetDatePoints()
        {
            return _context.Metrics
                .Where (w => w.Branch == null)
                .GroupBy(s => new { s.DateId, s.Date.DateTime })
                .Select(v => new ViewModels.DataPoint { DateId = v.Key.DateId, Date = v.Key.DateTime })
                .OrderBy(s => s.Date)
                .ToList();            
        }
        public IList<ViewModels.MetricsItem> GetSystemsByDate(int dateId)
        {
            var query = _context.Systems
                .SelectMany(s => s.Modules, (s, d) => new { System = s, MetricsList = d.Metrics })
                .SelectMany(s => s.MetricsList, (s, d) => new { System = s.System, MetricsItem = d })
                .Where(w => w.MetricsItem.BranchId == null && w.MetricsItem.DateId == dateId
                            && w.MetricsItem.Member == null && w.MetricsItem.Type == null && w.MetricsItem.Namespace == null)
                .GroupBy(g => new { g.System.SystemId, g.System.Name})
                .Select(s => new ViewModels.MetricsItem
                {
                    Id = s.Key.SystemId,
                    Name = s.Key.Name,
                    LinesOfCode = s.Sum(x => x.MetricsItem.LinesOfCode),
                    CodeCoverage = (int?) s.Average(x => x.MetricsItem.CodeCoverage) ?? 0,
                    MaintainabilityIndex = (int) s.Average(x => x.MetricsItem.MaintainabilityIndex),
                    ClassCoupling = s.Sum(x => x.MetricsItem.ClassCoupling),
                    CyclomaticComplexity = s.Sum(x => x.MetricsItem.CyclomaticComplexity),
                    DepthOfInheritance = s.Max(x => x.MetricsItem.DepthOfInheritance)
                });

            return query.ToList();            
        }

        public CodeCoverageSummary GetCoverageSummary(int numberOfDaysToReturn, Category category)
        {
            List<IdName> idNames;

            switch (category)
            {
                case Category.Systems:
                    idNames = Mapper.Map<List<IdName>>(_context.Systems);
                    break;
                case Category.Repos:
                    idNames = Mapper.Map<List<IdName>>(_context.Repos);
                    break;
                case Category.Teams:
                    idNames = Mapper.Map<List<IdName>>(_context.Teams);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category));
            }
            
            var dateFrom = DateTime.Now.AddDays(numberOfDaysToReturn * (-1));

            IQueryable<FactMetrics> metrics = _context.Metrics
                     .Where(m => m.Date.DateTime > dateFrom && m.BranchId == null && m.ModuleId != null && m.NamespaceId == null && m.TypeId == null && m.MemberId == null);
            
            var items = new List<ViewModels.CodeCoverageItem>();
            
            foreach (var idName in idNames)
            {
                IQueryable<FactMetrics> itemMetrics;
                if (category == Category.Systems)
                {
                    itemMetrics = metrics.Where(m => m.Module.Systems.Any(s => s.SystemId == idName.Id));
                }
                else if (category == Category.Repos)
                {
                    itemMetrics = metrics.Where(m => m.Module.RepoId == idName.Id);
                }
                else
                {
                    itemMetrics = metrics.Where(m => m.Module.TeamId == idName.Id);
                }
                   
                var item = new CodeCoverageItem
                {
                    Id = idName.Id,
                    Name = idName.Name,
                    DataPoints = itemMetrics
                        .Select(
                            s =>
                                new ViewModels.DataPoint
                                {
                                    DateId = s.DateId,
                                    Date = s.Date.DateTime,
                                    Value = s.CodeCoverage ?? 0
                                })
                        .GroupBy(g => g.Date)
                        .Select(
                            s => new ViewModels.DataPoint
                            {
                                DateId = s.Max(x => x.DateId),
                                Date = s.Key,
                                Value = (int)s.Average(x => x.Value)
                            }
                        )
                        .OrderBy(o => o.DateId)
                        .ToList()
                };

                items.Add(item);
            }

            return new CodeCoverageSummary {Items = items.OrderBy(s => s.Name).ToList()};
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }       
    }
}