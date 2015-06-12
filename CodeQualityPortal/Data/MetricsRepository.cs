using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public class MetricsRepository : IMetricsRepository
    {
        private readonly CodeQualityContext _context;
        private bool _disposed;

        public MetricsRepository() 
            : this (new CodeQualityCreateDatabaseIfNotExists())
        {
        }

        public MetricsRepository(IDatabaseInitializer<CodeQualityContext> context)
        {
            _context = new CodeQualityContext(context);
        }

        // 1 - systems
        public IList<ViewModels.SystemDefinition> GetSystems()
        {
            var items = _context.Systems.ToList();
            return Mapper.Map<IList<ViewModels.SystemDefinition>>(items);
        }

        public IList<TrendItem> GetModuleTrend(int? systemId, DateTime dateFrom, DateTime dateTo)
        {
            IList<IGrouping<DateTuple, FactMetrics>> groupedItems;
            if (systemId.HasValue)
            {
                groupedItems = _context.Metrics
                    .Where(w => w.ModuleId != null && w.NamespaceId == null && w.TypeId == null && w.MemberId == null
                            && w.Date.Date >= dateFrom && w.Date.Date <= dateTo
                            && w.Module.Systems.Any(a => a.SystemId == systemId.Value))
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                    .ToList();   
            }
            else
            {
                groupedItems = _context.Metrics
                    .Where(w => w.ModuleId != null && w.NamespaceId == null && w.TypeId == null && w.MemberId == null
                            && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                    .ToList();   
            }

            var items = Mapper.Map<IList<TrendItem>>(groupedItems);
            return items;
        }

        public IList<ModuleItem> GetModules(int? systemId, int dateId)
        {
            var queryItems = _context.Metrics
                .Where(w => w.ModuleId != null && w.NamespaceId == null && w.TypeId == null && w.MemberId == null
                            && w.DateId == dateId);

            if (systemId.HasValue)
            {
                queryItems = queryItems.Where(w => w.Module.Systems.Any(a => a.SystemId == systemId.Value));
            }
            
            var items = Mapper.Map<IList<ModuleItem>>(queryItems.ToList());
            return items;
        }

        // 2 - modules
        public IList<Module> GetModulesBySystem(int systemId)
        {
            IQueryable<DimModule> queryItems =  _context.Modules.Where(w => w.Systems.Any(a => a.SystemId == systemId));
            var items = Mapper.Map<IList<Module>>(queryItems.ToList());
            return items;
        }

        public IList<Module> GetAllModules()
        {
            return Mapper.Map<IList<Module>>(_context.Modules.ToList());
        }
        
        public IList<TrendItem> GetNamespaceTrend(int moduleId, DateTime dateFrom, DateTime dateTo)
        {
            var groupedItems = _context.Metrics
                .Where(w => w.ModuleId == moduleId && w.NamespaceId == null && w.TypeId == null && w.MemberId == null 
                    && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                .ToList();

            var items = Mapper.Map<IList<TrendItem>>(groupedItems);
            return items;                
        }
        
        public IList<NamespaceItem> GetNamespaces(int moduleId, int dateId)
        {
            var query = _context.Metrics
                .Where(w => w.DateId == dateId && w.ModuleId == moduleId && w.NamespaceId != null && w.TypeId == null && w.MemberId == null);

            var items = Mapper.Map<IList<NamespaceItem>>(query.ToList());
            return items;
        }


        // 3 - namespaces
        public IList<Namespace> GetNamespacesByModule(int moduleId)
        {
            var queryItems = _context.Modules
                .Where(w => w.ModuleId == moduleId)
                .SelectMany(s => s.Namespaces)
                .ToList();

            var items = Mapper.Map<IList<Namespace>>(queryItems);
            return items;
        }
        
        public IList<TrendItem> GetTypeTrend(int moduleId, int namespaceId, DateTime dateFrom, DateTime dateTo)
        {
            var groupedItems = _context.Metrics
                .Where(w => w.ModuleId == moduleId && w.NamespaceId == namespaceId && w.TypeId == null && w.MemberId == null
                        && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                .ToList();

            var items = Mapper.Map<IList<TrendItem>>(groupedItems);
            return items;
        }
        
        public IList<TypeItem> GetTypes(int moduleId, int namespaceId, int dateId)
        {
            var queryItems = _context.Metrics
                .Where(w => w.DateId == dateId && w.ModuleId == moduleId && w.NamespaceId == namespaceId && w.TypeId != null && w.MemberId == null)
                .ToList();

            var items = Mapper.Map<IList<TypeItem>>(queryItems);
            return items;
        }

        // 4 - types
        public IList<ViewModels.Type> GetTypesByNamespace(int moduleId, int namespaceid)
        {
            var queryItems = _context.Modules
                .Where(w => w.ModuleId == moduleId)
                .SelectMany(b => b.Namespaces)
                .Where(z => z.NamespaceId == namespaceid)
                .SelectMany(o => o.Types);

            var items = Mapper.Map<IList<ViewModels.Type>>(queryItems.ToList());
            return items;
        }

        public IList<TrendItem> GetMemberTrend(int moduleId, int namespaceid, int typeId, DateTime dateFrom, DateTime dateTo)
        {
            var groupedItems = _context.Metrics
                .Where(w => w.ModuleId == moduleId && w.NamespaceId == namespaceid && w.TypeId == typeId && w.MemberId == null &&
                    w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                .ToList();

            var items = Mapper.Map<IList<TrendItem>>(groupedItems);
            return items;
        }

        public IList<MemberItem> GetMembers(int moduleId, int namespaceid, int typeId, int dateId)
        {
            var queryItems = _context.Metrics
                .Where(w => w.ModuleId == moduleId && w.NamespaceId == namespaceid && w.TypeId == typeId && w.MemberId != null && w.DateId == dateId);

            var items = Mapper.Map<IList<MemberItem>>(queryItems);
            return items;
        }

        
        // 5 - Members
        public IList<Member> GetMembersByType(int moduleId, int namespaceid, int typeId)
        {
            var queryItems = _context.Modules
                .Where(x => x.ModuleId == moduleId)
                .SelectMany(b => b.Namespaces)
                .Where(z => z.NamespaceId == namespaceid)
                .SelectMany(r => r.Types)
                .Where(w => w.TypeId == typeId)
                .SelectMany(o => o.Members);

            var items = Mapper.Map<IList<Member>>(queryItems.ToList());
            return items;
        }


        public IList<TrendItem> GetSingleMemberTrend(int moduleId, int namespaceid, int typeId, int memberId, DateTime dateFrom, DateTime dateTo)
        {
            var groupedItems = _context.Metrics
                .Where(w => w.ModuleId == moduleId && w.NamespaceId == namespaceid && w.TypeId == typeId && w.MemberId == memberId
                    && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date }) // there's no point to group since it's the bottom
                .ToList();

            var items = Mapper.Map<IList<TrendItem>>(groupedItems);
            return items;
        }

        public IList<MemberSummary> GetWorst(DateTime dateFrom, DateTime dateTo, int topX)
        {
            var result = _context.Metrics
                .Where(w => w.Date.Date >= dateFrom && w.Date.Date <= dateTo && w.Member != null)
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
                .Select(s => s.Date)
                .OrderByDescending(s => s.Date)
                .FirstOrDefault();

            if (lastRunDate == null)
            {
                return null;
            }
                
            var modules = _context.Metrics
                .Where(w => w.DateId == lastRunDate.DateId && w.ModuleId != null && w.NamespaceId == null && w.TypeId == null && w.MemberId == null)
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
                    .Where(m => m.ModuleId != null && m.NamespaceId == null && m.TypeId == null && m.MemberId == null &&
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
                    .OrderBy(o => o.Key)
                    .Take(10)
                    .ToList();

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