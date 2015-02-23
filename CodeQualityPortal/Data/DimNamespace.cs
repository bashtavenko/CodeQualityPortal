using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimNamespace
    {
        public int NamespaceId { get; set; }
        public string Name { get; set; }
        public virtual List<DimModule> Modules { get; set; }
        public virtual List<DimType> Types { get; set; }        
        public virtual List<FactMetrics> Metrics { get; set; }

        public DimNamespace()
        {
            Types = new List<DimType>();
            Modules = new List<DimModule>();
        }
    }
}
