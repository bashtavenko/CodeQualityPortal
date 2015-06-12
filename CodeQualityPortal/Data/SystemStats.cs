namespace CodeQualityPortal.Data
{
    public class SystemStats
    {
        public DimSystem DimSystem { get; set; }
        public Trend MaintainabilityIndex { get; set; }
        public Trend LinesOfCode { get; set; }
        public Trend CodeCoverage { get; set; }

        public SystemStats()
        {
            DimSystem = new DimSystem();
            MaintainabilityIndex = new Trend();
            LinesOfCode = new Trend();
            CodeCoverage = new Trend();
        }
    }
}