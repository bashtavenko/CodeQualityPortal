using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimSystem
    {
        public int SystemId { get; set; }
        public string Name { get; set; }
        public virtual List<DimModule> Modules { get; set; }

        public DimSystem()
        {            
            Modules = new List<DimModule>();
        }
    }
}
