using System;
using System.Collections.Generic;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public interface IMetricsRepository
    {
        IList<string> GetTags();
        IList<TrendItem> GetModuleTrend(string tag, DateTime dateFrom, DateTime dateTo);
        IList<ModuleItem> GetModules(string tag, int dateId);

        IList<Module> GetModulesByTag(string tag);
        IList<TrendItem> GetNamespaceTrend(int moduleId, DateTime dateFrom, DateTime dateTo);
        IList<NamespaceItem> GetNamespaces(int moduleId, int dateId);
    }
}