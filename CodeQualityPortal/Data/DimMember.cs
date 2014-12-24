using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimMember
    {
        public int MemberId { get; set; }
        public int TypeId { get; set; }
        public virtual DimType Type { get; set; }
        public string Name { get; set; }                   
        public string File { get; set; }
        public int? Line { get; set; }
        public virtual List<FactMetrics> Metrics { get; set; }
    }
}
