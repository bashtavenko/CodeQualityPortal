namespace CodeQualityPortal.ViewModels
{
    public class SystemStats
    {
        public int SystemId { get; set; }
        public string SystemName { get; set; }
        public Trend MaintainabilityIndex { get; set; }
        public Trend LinesOfCode { get; set; }
        public Trend CodeCoverage { get; set; }
    }
}