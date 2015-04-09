using System;
using System.Collections.Generic;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public interface IMetricsRepository : IDisposable
    {
        // 1 - systems
        IList<ViewModels.SystemDefinition> GetSystems();
        IList<TrendItem> GetModuleTrend(int? systemId, DateTime dateFrom, DateTime dateTo);
        IList<ModuleItem> GetModules(int? systemId, int dateId);

        // 2 - modules
        IList<Module> GetModulesBySystem(int systemId);
        IList<Module> GetAllModules();
        IList<TrendItem> GetNamespaceTrend(int moduleId, DateTime dateFrom, DateTime dateTo);
        IList<NamespaceItem> GetNamespaces(int moduleId, int dateId);

        // 3 - namespaces
        IList<Namespace> GetNamespacesByModule(int moduleId);
        IList<TrendItem> GetTypeTrend(int moduleId, int namespaceId, DateTime dateFrom, DateTime dateTo);
        IList<TypeItem> GetTypes(int moduleId, int namespaceId, int dateId);
        
        // 4 - types
        IList<ViewModels.Type> GetTypesByNamespace(int moduleId, int namespaceid);
        IList<TrendItem> GetMemberTrend(int moduleId, int namespaceid, int typeId, DateTime dateFrom, DateTime dateTo);
        IList<MemberItem> GetMembers(int moduleId, int namespaceid, int typeId, int dateId);

        // 5 - members
        IList<Member> GetMembersByType(int moduleId, int namespaceid, int typeId);
        IList<TrendItem> GetSingleMemberTrend(int moduleId, int namespaceid, int typeId, int memberId, DateTime dateFrom, DateTime dateTo);

        IList<MemberSummary> GetWorst(DateTime dateFrom, DateTime to, int topX);
        KeyStats GetLatestKeyStats();
    }
}