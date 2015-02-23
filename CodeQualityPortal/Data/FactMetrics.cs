namespace CodeQualityPortal.Data
{
    public class FactMetrics
    {
        public int MetricsId { get; set; }

        public int DateId { get; set; }
        public virtual DimDate Date { get; set; }

        public int? ModuleId { get; set; }
        public virtual DimModule Module { get; set; }

        public int? NamespaceId { get; set; }
        public virtual DimNamespace Namespace { get; set; }

        public int? TypeId { get; set; }
        public virtual DimType Type { get; set; }

        public int? MemberId { get; set; }
        public virtual DimMember Member { get; set; }

        public int? MaintainabilityIndex { get; set; }
        public int? CyclomaticComplexity { get; set; }
        public int? ClassCoupling { get; set; }
        public int? DepthOfInheritance { get; set; } // members don't have it
        public int? LinesOfCode { get; set; }
        public int? CodeCoverage { get; set; }               
    }
}
