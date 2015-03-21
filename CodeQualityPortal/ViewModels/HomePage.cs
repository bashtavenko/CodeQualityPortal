using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace CodeQualityPortal.ViewModels
{
    public class HomePage
    {
        public int? MaintainabilityIndex { get; private set; }
        public int? CodeCoverage { get; private set; }
        public int? LinesOfCode { get; private set; }
        public ICollection<FileCodeChurn> ChurnItems { get; private set; }
        public ICollection<MemberSummary> MemberItems { get; private set; }

        public HomePage(KeyStats keyStats, ICollection<FileCodeChurn> churnItems, ICollection<MemberSummary> memberItems)
        {
            if (keyStats != null)
            {
                MaintainabilityIndex = keyStats.MaintainabilityIndex;
                CodeCoverage = keyStats.CodeCoverage;
                LinesOfCode = keyStats.LinesOfCode;
            }

            ChurnItems = churnItems;
            MemberItems = memberItems;
        }
    }
}