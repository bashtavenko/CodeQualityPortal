namespace CodeQualityPortal.ViewModels
{
    public class DiffSummaryItem
    {
        public string MetricName{ get; set; }
        public int? BranchAMetricValue { get; set; }
        public int? BranchBMetricValue { get; set; }
        public int? DiffValue { get; set; }
    }
}