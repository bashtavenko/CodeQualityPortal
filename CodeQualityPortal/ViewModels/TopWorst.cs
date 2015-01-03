using System.Collections.Generic;

namespace CodeQualityPortal.ViewModels
{
    public class TopWorst
    {
        public ICollection<FileChurnSummary> ChurnItems { get; set; }
        public ICollection<MemberSummary> MemberItems { get; set; }
    }
}