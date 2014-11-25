using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeQualityPortal.ViewModels
{
    public class CodeChurnByDate
    {
        public int? DateId { get; set; }
        public DateTime Date { get; set; }
        public int? LinesAdded { get; set; }
        public int? LinesModified { get; set; }
        public int? LinesDeleted { get; set; }
        public int? TotalChurn { get; set; }
    }
}