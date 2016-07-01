using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimTeam
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public virtual List<DimModule> Modules { get; set; }

        public DimTeam()
        {            
            Modules = new List<DimModule>();
        }
    }
}