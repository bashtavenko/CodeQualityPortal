using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimFile
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Url { get; set; }
        public virtual List<FactCodeChurn> Churn { get; set; }
        public virtual List<DimCommit> Commits { get; set; }

        public DimFile()
        {
            this.Churn = new List<FactCodeChurn>();
            this.Commits = new List<DimCommit>();
        }

        public DimFile(string fileName)
            : this()
        {
            this.FileExtension = System.IO.Path.GetExtension(fileName);
        }
    }
}
