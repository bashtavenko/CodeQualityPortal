using System;
using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimBranch
    {
        public int BranchId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<FactMetrics> Metrics { get; set; }
        
        public DimBranch()
        {
            Metrics = new List<FactMetrics>();    
        }
    }
}