using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using CodeQualityPortal.ViewModels;

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
                var groupedItems = context.Metrics
                    .Where(w => w.Module.Target.Tag == tag && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                    .ToList();

                var items = Mapper.Map<IList<TrendItem>>(groupedItems);
                return items;
            }
        }

        public IList<ModuleItem> GetModules(string tag, int dateId)
        {
            using (var context = new CodeQualityContext())
            {                
                var queryItems = context.Metrics
                    .Where(w => w.Module.Target.Tag == tag && w.DateId == dateId)
                    .ToList();

                var items = Mapper.Map<IList<ModuleItem>>(queryItems);
                return items;
            }
        }

        public IList<Module> GetModulesByTag(string tag)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Modules
                    .Where(w => w.Target.Tag == tag)
                    .ToList();

                var items = Mapper.Map<IList<Module>>(queryItems);
                return items;
            }
        }
        public IList<TrendItem> GetNamespaceTrend(int moduleId, DateTime dateFrom, DateTime dateTo)
        {
            using (var context = new CodeQualityContext())
            {
                var groupedItems = context.Metrics
                    .Where(w => w.Namespace.ModuleId == moduleId && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                    .ToList();

                var items = Mapper.Map<IList<TrendItem>>(groupedItems);
                return items;                
            }            
        }
        public IList<NamespaceItem> GetNamespaces(int moduleId, int dateId)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Metrics
                    .Where(w => w.Namespace.ModuleId == moduleId && w.DateId == dateId)
                    .ToList();

                var items = Mapper.Map<IList<NamespaceItem>>(queryItems);
                return items;
            }
        }
    }
}