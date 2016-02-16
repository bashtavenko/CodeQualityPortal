using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.ViewModels
{
    public class SystemGraphSummary
    {
        public int Average
        {
            get { return (int) Items.Average(s => s.AverageValue); }
        }
        public IList<SystemGraph> Items { get; set; } 
    }
}