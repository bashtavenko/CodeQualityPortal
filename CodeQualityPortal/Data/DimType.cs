using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimType
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public virtual List<DimNamespace> Namespaces { get; set; }
        public virtual List<DimMember> Members { get; set; }
        public virtual List<FactMetrics> Metrics { get; set; }

        public DimType()
        {
            Members = new List<DimMember>();
            Namespaces = new List<DimNamespace>();
        }
    }
}
