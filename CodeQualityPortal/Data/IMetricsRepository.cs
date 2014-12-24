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
    }
}