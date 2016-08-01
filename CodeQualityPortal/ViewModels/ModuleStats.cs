namespace CodeQualityPortal.ViewModels
{
    public class ModuleStats
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LinesOfCode { get; set; }
        public int? CodeCoverage { get; set; }
    }
}