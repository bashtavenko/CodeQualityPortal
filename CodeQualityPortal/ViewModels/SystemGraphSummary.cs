using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.ViewModels
{
    public class SystemGraphSummary
    {
        public int Average
        {
            get { return Items.Any() ? (int) Items.Average(s => s.AverageValue) : default(int); }
        }
        public IList<SystemGraph> Items { get; set; } 
    }
}