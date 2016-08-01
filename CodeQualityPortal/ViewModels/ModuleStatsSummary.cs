using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.ViewModels
{
    public class ModuleStatsSummary
    {
        public int LinesOfCode
        {
            get { return Items.Any() ? (int)Items.Sum(s => s.LinesOfCode ?? 0) : default(int); }
        }

        public int CodeCoverage
        {
            get { return Items.Any() ? (int)Items.Average(s => s.CodeCoverage ?? 0) : default(int); }
        }
        public IList<ModuleStats> Items { get; set; }

        public ModuleStatsSummary(IList<ModuleStats> items)
        {
            Items = items;
        }
    }
}