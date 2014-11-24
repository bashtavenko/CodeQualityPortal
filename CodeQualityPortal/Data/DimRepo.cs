using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimRepo
    {
        public int RepoId { get; set; }
        public string Name { get; set; }
        public List<DimCommit> Commits { get; set; }

        public DimRepo()
        {
            this.Commits = new List<DimCommit>();
        }
    }
}
