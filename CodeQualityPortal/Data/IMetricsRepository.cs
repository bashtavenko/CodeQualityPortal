using System;
using System.Collections.Generic;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public interface IMetricsRepository
    {
        // 1 - tags
        IList<string> GetTags();
        IList<TrendItem> GetModuleTrend(string tag, DateTime dateFrom, DateTime dateTo);
        IList<ModuleItem> GetModules(string tag, int dateId);

        // 2 - modules
        IList<Module> GetModulesByTag(string tag);
        IList<TrendItem> GetNamespaceTrend(int moduleId, DateTime dateFrom, DateTime dateTo);
        IList<NamespaceItem> GetNamespaces(int moduleId, int dateId);

        // 3 - namespaces
        IList<Namespace> GetNamespacesByModule(int moduleId);
        IList<TrendItem> GetTypeTrend(int namespaceId, DateTime dateFrom, DateTime dateTo);
        IList<TypeItem> GetTypes(int namespaceId, int dateId);
        
        // 4 - types
        IList<ViewModels.Type> GetTypesByNamespace(int namespaceid);
        IList<TrendItem> GetMemberTrend(int typeId, DateTime dateFrom, DateTime dateTo);
        IList<MemberItem> GetMembers(int typeId, int dateId);

        // 5 - members
        IList<Member> GetMembersByType(int typeId);
        IList<TrendItem> GetSingleMemberTrend(int memberId, DateTime dateFrom, DateTime dateTo);

        IList<MemberSummary> GetWorst(DateTime dateFrom, DateTime to, int topX);
    }
}