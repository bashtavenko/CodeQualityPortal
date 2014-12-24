using System;

namespace CodeQualityPortal.ViewModels
{
    public class MetricsItem
    {        
        public int? MaintainabilityIndex { get; set; }
        public int? CyclomaticComplexity { get; set; }
        public int? ClassCoupling { get; set; }
        public int? DepthOfInheritance { get; set; }
        public int? LinesOfCode { get; set; }
    }
}