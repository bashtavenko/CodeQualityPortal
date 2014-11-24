using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimCommit
    {
        public int CommitId { get; set; }
        public string Sha { get; set; }
        public string Url { get; set; }
        public int RepoId { get; set; }
        public virtual DimRepo Repo { get; set; }
        public List<FactCodeChurn> Churn { get; set; }

        public List<DimFile> Files { get; set; }

        public DimCommit()
        {
            this.Files = new List<DimFile>();
            this.Churn = new List<FactCodeChurn>();
        }
    }
}
