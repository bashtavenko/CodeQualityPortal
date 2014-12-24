using CodeQualityPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.Data
{
    public class MetricsRepository : IMetricsRepository
    {
        public IList<string> GetTags()
        {
            using (var context = new CodeQualityContext())
            {
                return context.Targets.GroupBy(g => g.Tag).Select(s => s.Key).ToList();
            }
        }

        public IList<TrendItem> GetModuleTrend(string tag, DateTime dateFrom, DateTime dateTo)
        {
            using (var context = new CodeQualityContext())
            {
                var query = context.Metrics
                    .Where(w => w.Module.Target.Tag == tag && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new { g.DateId, g.Date.DateTime })
                    .Select(s => new TrendItem
                    {
                        DateId = s.Key.DateId,
                        Date = s.Key.DateTime,
                        MaintainabilityIndex = (int?) s.Average(a => a.MaintainabilityIndex),
                        CyclomaticComplexity = s.Sum(a => a.CyclomaticComplexity),
                        ClassCoupling = s.Sum(a => a.ClassCoupling),
                        DepthOfInheritance = s.Max(a => a.DepthOfInheritance),
                        LinesOfCode = s.Sum(a => a.LinesOfCode)
                    });
                return query.ToList();
            }            
        }

        public IList<ModuleItem> GetModules(string tag, int dateId)
        {
            using (var context = new CodeQualityContext())
            {                
                var query = context.Metrics
                    .Where(w => w.Module.Target.Tag == tag && w.DateId == dateId)
                    .Select(s => new ModuleItem
                    {
                        Id = s.Module.ModuleId,
                        Name = s.Module.Name,
                        AssemblyVersion = s.Module.AssemblyVersion,
                        MaintainabilityIndex = s.MaintainabilityIndex,
                        CyclomaticComplexity = s.CyclomaticComplexity,
                        ClassCoupling = s.ClassCoupling,
                        DepthOfInheritance = s.DepthOfInheritance,
                        LinesOfCode = s.LinesOfCode
                    });
                return query.ToList();
            }
        }
    }
}