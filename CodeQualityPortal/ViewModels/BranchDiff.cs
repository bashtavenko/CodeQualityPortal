using System.Collections.Generic;

namespace CodeQualityPortal.ViewModels
{
    public class BranchDiff
    {
        // Two ways to display summary items, one is an extra row and another one
        // as a table. Second UI is helpful because Wijmo FlexGrid still doesn't suppport custom footers.
        public BranchDiffItem DiffSummary { get; set; }
        public IList<DiffSummaryItem> DiffSummaryItems { get; set; }

        public IList<BranchDiffItem> ModuleDiff { get; set; }
    }
}