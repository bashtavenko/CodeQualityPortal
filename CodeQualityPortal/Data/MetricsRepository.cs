using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using AutoMapper;

using CodeQualityPortal.ViewModels;


namespace CodeQualityPortal.Data
{
    public class MetricsRepository : IMetricsRepository
    {
        // 1 - systems
        public IList<string> GetSystems()
        {
            using (var context = new CodeQualityContext())
            {
                return context.Targets.GroupBy(g => g.Tag).Select(s => s.Key).ToList();
            }
        }

        public IList<TrendItem> GetModuleTrend(int? systemId, DateTime dateFrom, DateTime dateTo)
        {
            using (var context = new CodeQualityContext())
            {
                var groupedItems = context.Metrics
                    .Where(w => w.ModuleId != null && w.NamespaceId == null && w.TypeId == null && w.MemberId == null
                           && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                    .ToList();

                var items = Mapper.Map<IList<TrendItem>>(groupedItems);
                return items;
            }
        }

        public IList<ModuleItem> GetModules(int? systemId, int dateId)
        {
            using (var context = new CodeQualityContext())
            {                
                var queryItems = context.Metrics
                    .Where(w => w.ModuleId != null && w.NamespaceId == null && w.TypeId == null && w.MemberId == null
                        && w.DateId == dateId)
                    .ToList();

                var items = Mapper.Map<IList<ModuleItem>>(queryItems);
                return items;
            }
        }

        // 2 - modules
        public IList<Module> GetModulesBySystem(int? systemId)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Modules
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
                    .Where(w => w.ModuleId == moduleId && w.NamespaceId == null && w.TypeId == null && w.MemberId == null 
                        && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
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
                var query = context.Metrics
                    .Where(w => w.DateId == dateId && w.ModuleId == moduleId && w.NamespaceId != null && w.TypeId == null && w.MemberId == null);

                var items = Mapper.Map<IList<NamespaceItem>>(query.ToList());
                return items;
            }
        }


        // 3 - namespaces
        public IList<Namespace> GetNamespacesByModule(int moduleId)
        {
            using (var context = new CodeQualityContext())
            {   
                var queryItems = context.Modules
                    .Where(w => w.ModuleId == moduleId)
                    .SelectMany(s => s.Namespaces)
                    .ToList();

                var items = Mapper.Map<IList<Namespace>>(queryItems);
                return items;
            }            
        }
        
        public IList<TrendItem> GetTypeTrend(int moduleId, int namespaceId, DateTime dateFrom, DateTime dateTo)
        {
            using (var context = new CodeQualityContext())
            {
                var groupedItems = context.Metrics
                    .Where(w => w.ModuleId == moduleId && w.NamespaceId == namespaceId && w.TypeId == null && w.MemberId == null
                            && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                    .ToList();

                var items = Mapper.Map<IList<TrendItem>>(groupedItems);
                return items;
            }    
        }
        
        public IList<TypeItem> GetTypes(int moduleId, int namespaceId, int dateId)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Metrics
                    .Where(w => w.DateId == dateId && w.ModuleId == moduleId && w.NamespaceId == namespaceId && w.TypeId != null && w.MemberId == null)
                    .ToList();

                var items = Mapper.Map<IList<TypeItem>>(queryItems);
                return items;
            }            
        }

        // 4 - types
        public IList<ViewModels.Type> GetTypesByNamespace(int moduleId, int namespaceid)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Modules
                    .Where(w => w.ModuleId == moduleId)
                    .SelectMany(b => b.Namespaces)
                    .Where(z => z.NamespaceId == namespaceid)
                    .SelectMany(o => o.Types);

                var items = Mapper.Map<IList<ViewModels.Type>>(queryItems.ToList());
                return items;
            }            
        }

        public IList<TrendItem> GetMemberTrend(int moduleId, int namespaceid, int typeId, DateTime dateFrom, DateTime dateTo)
        {
            using (var context = new CodeQualityContext())
            {
                var groupedItems = context.Metrics
                    .Where(w => w.ModuleId == moduleId && w.NamespaceId == namespaceid && w.TypeId == typeId && w.MemberId == null &&
                        w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date })
                    .ToList();

                var items = Mapper.Map<IList<TrendItem>>(groupedItems);
                return items;
            }
        }

        public IList<MemberItem> GetMembers(int moduleId, int namespaceid, int typeId, int dateId)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Metrics
                    .Where(w => w.ModuleId == moduleId && w.NamespaceId == namespaceid && w.TypeId == typeId && w.MemberId != null && w.DateId == dateId);

                var items = Mapper.Map<IList<MemberItem>>(queryItems);
                return items;
            }                        
        }

        
        // 5 - Members
        public IList<Member> GetMembersByType(int moduleId, int namespaceid, int typeId)
        {
            using (var context = new CodeQualityContext())
            {
                var queryItems = context.Modules
                    .Where(x => x.ModuleId == moduleId)
                    .SelectMany(b => b.Namespaces)
                    .Where(z => z.NamespaceId == namespaceid)
                    .SelectMany(r => r.Types)
                    .Where(w => w.TypeId == typeId)
                    .SelectMany(o => o.Members);

                var items = Mapper.Map<IList<Member>>(queryItems.ToList());
                return items;
            }                        
        }


        public IList<TrendItem> GetSingleMemberTrend(int moduleId, int namespaceid, int typeId, int memberId, DateTime dateFrom, DateTime dateTo)
        {
            using (var context = new CodeQualityContext())
            {
                var groupedItems = context.Metrics
                    .Where(w => w.ModuleId == moduleId && w.NamespaceId == namespaceid && w.TypeId == typeId && w.MemberId == memberId
                        && w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
                    .GroupBy(g => new DateTuple { DateId = g.DateId, DateTime = g.Date.Date }) // there's no point to group since it's the bottom
                    .ToList();

                var items = Mapper.Map<IList<TrendItem>>(groupedItems);
                return items;
            }            
        }

        public IList<MemberSummary> GetWorst(DateTime dateFrom, DateTime dateTo, int topX)
        {
            using (var context = new CodeQualityContext())
            {
                var result = context.Metrics
                    .Where(w => w.Date.Date >= dateFrom && w.Date.Date <= dateTo)
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
        }

        public KeyStats GetLatestKeyStats()
        {
            using (var context = new CodeQualityContext())
            {
                var lastRunDate = context
                    .Metrics
                    .Select(s => s.Date)
                    .OrderByDescending(s => s.Date)
                    .FirstOrDefault();

                if (lastRunDate == null)
                {
                    return null;
                }
                
                var modules = context.Metrics
                    .Where(w => w.DateId == lastRunDate.DateId && w.ModuleId != null && w.NamespaceId == null && w.TypeId == null && w.MemberId == null)
                    .Select(s => s)
                    .ToList();

                var stats = new KeyStats(lastRunDate.Date, modules);
                return stats;
            }
        }
    }
}