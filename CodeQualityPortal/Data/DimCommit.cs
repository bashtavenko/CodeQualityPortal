using System;
using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimCommit
    {
        public int CommitId { get; set; }
        public string Sha { get; set; }
        public string Url { get; set; }
        public string Committer { get; set; }
        public string CommitterAvatarUrl { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int RepoId { get; set; }
        public virtual DimRepo Repo { get; set; }
        public virtual List<FactCodeChurn> Churn { get; set; }
        public virtual List<DimFile> Files { get; set; }

        public DimCommit()
        {
            this.Files = new List<DimFile>();
            this.Churn = new List<FactCodeChurn>();
        }
    }
}
