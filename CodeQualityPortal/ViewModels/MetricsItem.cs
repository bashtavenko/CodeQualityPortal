using System;

namespace CodeQualityPortal.ViewModels
{
    public class MetricsItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MaintainabilityIndex { get; set; }
        public int? CyclomaticComplexity { get; set; }
        public int? ClassCoupling { get; set; }
        public int? DepthOfInheritance { get; set; }
        public int? LinesOfCode { get; set; }
        public int? CodeCoverage { get; set; }

        public bool IsNotZero => MaintainabilityIndex > 0 || CyclomaticComplexity > 0 || ClassCoupling > 0 || DepthOfInheritance > 0 || LinesOfCode > 0 || CodeCoverage > 0;
    }
}