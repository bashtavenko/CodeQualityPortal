using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public class BranchRepository : IBranchRepository
    {
        private readonly CodeQualityContext _context;
        private bool _disposed;

        public BranchRepository() 
            : this (new CodeQualityCreateDatabaseIfNotExists())
        {
        }

        public BranchRepository(IDatabaseInitializer<CodeQualityContext> context)
        {
            _context = new CodeQualityContext(context);
        }

        public IList<ViewModels.Branch> GetBranches()
        {
            var items = _context.Branches.OrderBy(s => s.Name).ToList();
            return Mapper.Map<List<ViewModels.Branch>>(items);
        }

        public IList<ViewModels.DataPoint> GetBranchCollectionDates(int? branchId)
        {
            var items = _context.Metrics
                .Where(w => w.BranchId == branchId)
                .GroupBy(s => new { s.DateId, s.Date.DateTime })
                .Select(v => new ViewModels.DataPoint { DateId = v.Key.DateId, Date = v.Key.DateTime })
                .OrderByDescending (s => s.Date)
                .Take(10)
                .ToList();           
            return items;
        }

        public BranchDiff GetBranchDiff(int? branchAId, int dateAId, int? branchBId, int dateBId)
        {
            IEnumerable<ModuleItem> modulesA = GetModulesByBranchAndDate(branchAId, dateAId);
            IEnumerable<ModuleItem> modulesB = GetModulesByBranchAndDate(branchBId, dateBId);

            List<BranchDiffItem> moduleDiff = modulesA.Join(modulesB, // Join to moduleB
                a => a.Id, // Source
                b => b.Id, // Target
                (a, b) => new BranchDiffItem
                {
                    BranchA = a,
                    BranchB = b,
                    Diff = CalculateDiff(a, b)
                }) // Result
                .Where(w => w.Diff.IsNotZero) // Only delta
                .ToList();
            
            var diffSummary = new BranchDiffItem
            {
                BranchA = CalculateSummary(moduleDiff.Select(s => s.BranchA).ToList()),
                BranchB = CalculateSummary(moduleDiff.Select(s => s.BranchB).ToList()),
            };
            diffSummary.Diff = CalculateDiff(diffSummary.BranchA, diffSummary.BranchB);
            var diffSummaryItems = new List<DiffSummaryItem>
            {
                new DiffSummaryItem
                {
                    MetricName = "Lines of code (LOC)",
                    BranchAMetricValue = diffSummary.BranchA.LinesOfCode,
                    BranchBMetricValue = diffSummary.BranchB.LinesOfCode,
                    DiffValue = diffSummary.Diff.LinesOfCode
                },
                new DiffSummaryItem
                {
                    MetricName = "Code coverage (CCE)",
                    BranchAMetricValue = diffSummary.BranchA.CodeCoverage,
                    BranchBMetricValue = diffSummary.BranchB.CodeCoverage,
                    DiffValue = diffSummary.Diff.CodeCoverage
                },
                new DiffSummaryItem
                {
                    MetricName = "Cyclomatic complexity (CCX)",
                    BranchAMetricValue = diffSummary.BranchA.CyclomaticComplexity,
                    BranchBMetricValue = diffSummary.BranchB.CyclomaticComplexity,
                    DiffValue = diffSummary.Diff.CyclomaticComplexity
                },
                new DiffSummaryItem
                {
                    MetricName = "Maintanability index (MIX)",
                    BranchAMetricValue = diffSummary.BranchA.MaintainabilityIndex,
                    BranchBMetricValue = diffSummary.BranchB.MaintainabilityIndex,
                    DiffValue = diffSummary.Diff.MaintainabilityIndex
                },
                new DiffSummaryItem
                {
                    MetricName = "Depth of inheritance (INH)",
                    BranchAMetricValue = diffSummary.BranchA.DepthOfInheritance,
                    BranchBMetricValue = diffSummary.BranchB.DepthOfInheritance,
                    DiffValue = diffSummary.Diff.DepthOfInheritance
                },
                new DiffSummaryItem
                {
                    MetricName = "Class coupling (CCG)",
                    BranchAMetricValue = diffSummary.BranchA.ClassCoupling,
                    BranchBMetricValue = diffSummary.BranchB.ClassCoupling,
                    DiffValue = diffSummary.Diff.ClassCoupling
                },
            };
            
            return new BranchDiff
            {
                DiffSummary = diffSummary,
                DiffSummaryItems = diffSummaryItems,
                ModuleDiff = moduleDiff
            };
        }

        private IEnumerable<ModuleItem> GetModulesByBranchAndDate(int? branchId, int dateId)
        {
            return _context.Modules
                .SelectMany(s => s.Metrics, (m, f) => new
                {
                    Metrics = f,
                    ModuleItem = new ModuleItem() // Can't use Automaper in LINQ
                    {
                        Id = m.ModuleId,
                        Name = m.Name,
                        AssemblyVersion = m.AssemblyVersion,
                        LinesOfCode = f.LinesOfCode,
                        ClassCoupling = f.ClassCoupling,
                        CyclomaticComplexity = f.CyclomaticComplexity,
                        DepthOfInheritance = f.DepthOfInheritance,
                        CodeCoverage = f.CodeCoverage,
                        MaintainabilityIndex = f.MaintainabilityIndex
                    }
                })
                .Where(w => w.Metrics.BranchId == branchId && w.Metrics.DateId == dateId && w.Metrics.Module != null && w.Metrics.Namespace == null && w.Metrics.Type == null & w.Metrics.Member == null)
                .Select(s => s.ModuleItem);
        }

        private ModuleItem CalculateDiff(ModuleItem moduleA, ModuleItem moduleB)
        {
            return new ModuleItem
            {
                LinesOfCode = moduleB.LinesOfCode - moduleA.LinesOfCode,
                MaintainabilityIndex = moduleB.MaintainabilityIndex - moduleA.MaintainabilityIndex,
                CodeCoverage = moduleB.CodeCoverage - moduleA.CodeCoverage,
                CyclomaticComplexity = moduleB.CyclomaticComplexity - moduleA.CyclomaticComplexity,
                ClassCoupling = moduleB.ClassCoupling - moduleA.ClassCoupling,
                DepthOfInheritance = moduleB.DepthOfInheritance - moduleA.DepthOfInheritance
            };
        }

        private ModuleItem CalculateSummary(IList<ModuleItem> items)
        {
            return new ModuleItem
            {
                LinesOfCode = items.Sum(x => x.LinesOfCode),
                CodeCoverage = (int?)items.Average(x => x.CodeCoverage) ?? 0,
                MaintainabilityIndex = (int?)items.Average(x => x.MaintainabilityIndex) ?? 0,
                ClassCoupling = items.Sum(x => x.ClassCoupling),
                CyclomaticComplexity = items.Sum(x => x.CyclomaticComplexity),
                DepthOfInheritance = items.Max(x => x.DepthOfInheritance)
            };
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