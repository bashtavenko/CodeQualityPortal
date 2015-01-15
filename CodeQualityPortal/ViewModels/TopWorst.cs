using System.Collections.Generic;

namespace CodeQualityPortal.ViewModels
{
    public class TopWorst
    {
        public ICollection<FileCodeChurn> ChurnItems { get; set; }
        public ICollection<MemberSummary> MemberItems { get; set; }
    }
}