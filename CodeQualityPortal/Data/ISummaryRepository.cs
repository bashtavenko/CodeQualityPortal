using System;
using System.Collections.Generic;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Data
{
    public interface ISummaryRepository : IDisposable
    {
        IList<MemberSummary> GetWorst(DateTime dateFrom, DateTime to, int topX);
        KeyStats GetLatestKeyStats();
        IList<ViewModels.SystemStats> GetLatestSystemStats();
        IList<ViewModels.DataPoint> GetDatePoints();
        IList<MetricsItem> GetSystemsByDate(int dateId);
        CodeCoverageSummary GetCoverageSummary(int numberOfDaysToReturn, Category category);
        ModuleStatsSummary GetModuleStatsByCategoryAndDate(Category category, int categoryId, int dateId);
    }
}
