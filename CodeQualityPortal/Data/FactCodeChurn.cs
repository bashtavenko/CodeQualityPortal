using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQualityPortal.Data
{
    public class FactCodeChurn
    {
        public int CodeChurnId { get; set; }
        public int CommitId { get; set; }
        public virtual DimCommit Commit { get; set; }
        public int? FileId { get; set; }
        public virtual DimFile File { get; set; }
        public int DateId { get; set; }
        public virtual DimDate Date { get; set; }
        public int LinesAdded { get; set; }
        public int LinesModified { get; set; }
        public int LinesDeleted { get; set; }
        public int TotalChurn { get; set; }
    }
}
