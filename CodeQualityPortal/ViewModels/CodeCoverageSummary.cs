using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.ViewModels
{
    public class CodeCoverageSummary
    {
        public int Average
        {
            get { return Items.Any() ? (int) Items.Average(s => s.AverageValue) : default(int); }
        }
        public IList<CodeCoverageItem> Items { get; set; } 
    }
}