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
        IList<ViewModels.MetricsItem> GetSystemsByDate(int dateId);
        SystemGraphSummary GetCoverageBySystems(int numberOfDaysToReturn);
    }
}
