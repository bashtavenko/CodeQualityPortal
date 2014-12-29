using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public class MetricsRepository : IMetricsRepository
    {
        // 1 - tags
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

        // 2 - modules
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
                    .Where(w => w.ModuleId == moduleId && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
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


        // 3 - namespaces
        public IList<Namespace> GetNamespacesByModule(int moduleId)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Namespaces
                    .Where(w => w.ModuleId == moduleId)
                    .ToList();

                var items = Mapper.Map<IList<Namespace>>(queryItems);
                return items;
            }            
        }

        public IList<TrendItem> GetTypeTrend(int namespaceId, DateTime dateFrom, DateTime dateTo)
        {
            using (var context = new CodeQualityContext())
            {
                var groupedItems = context.Metrics
                    .Where(w => w.NamespaceId == namespaceId && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                    .ToList();

                var items = Mapper.Map<IList<TrendItem>>(groupedItems);
                return items;
            }    
        }

        public IList<TypeItem> GetTypes(int namespaceId, int dateId)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Metrics
                    .Where(w => w.Type.NamespaceId == namespaceId && w.DateId == dateId)
                    .ToList();

                var items = Mapper.Map<IList<TypeItem>>(queryItems);
                return items;
            }            
        }

        // 4 - types
        public IList<ViewModels.Type> GetTypesByNamespace(int namespaceid)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Types
                    .Where(w => w.NamespaceId == namespaceid)
                    .ToList();

                var items = Mapper.Map<IList<ViewModels.Type>>(queryItems);
                return items;
            }            
        }

        public IList<TrendItem> GetMemberTrend(int typeId, DateTime dateFrom, DateTime dateTo)
        {
            using (var context = new CodeQualityContext())
            {
                var groupedItems = context.Metrics
                    .Where(w => w.TypeId == typeId && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                    .ToList();

                var items = Mapper.Map<IList<TrendItem>>(groupedItems);
                return items;
            }
        }

        public IList<MemberItem> GetMembers(int typeId, int dateId)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Metrics
                    .Where(w => w.TypeId == typeId && w.DateId == dateId)
                    .ToList();

                var items = Mapper.Map<IList<MemberItem>>(queryItems);
                return items;
            }                        
        }

        
        // 5 - Members
        public IList<Member> GetMembersByType(int typeId)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Members
                    .Where(w => w.TypeId == typeId)
                    .ToList();

                var items = Mapper.Map<IList<Member>>(queryItems);
                return items;
            }                        
        }
                      

        public IList<TrendItem> GetSingleMemberTrend(int memberId, DateTime dateFrom, DateTime dateTo)
        {
            using (var context = new CodeQualityContext())
            {
                var groupedItems = context.Metrics
                    .Where(w => w.MemberId == memberId && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date }) // there's no point to group since it's the bottom
                    .ToList();

                var items = Mapper.Map<IList<TrendItem>>(groupedItems);
                return items;
            }            
        }
    }
}