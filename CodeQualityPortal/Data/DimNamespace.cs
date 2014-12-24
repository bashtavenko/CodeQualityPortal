using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimNamespace
    {
        public int NamespaceId { get; set; }
        public int ModuleId { get; set; }
        public virtual DimModule Module { get; set; }
        public string Name { get; set; }     
        public List<DimType> Types { get; set; }        
        public virtual List<FactMetrics> Metrics { get; set; }

        public DimNamespace()
        {
            Types = new List<DimType>();
        }
    }
}
