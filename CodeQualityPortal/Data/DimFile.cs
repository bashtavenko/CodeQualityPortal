using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public class DimFile
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Url { get; set; }
        public int CommitId { get; set; }
        public virtual DimCommit Commit { get; set; }
        public List<FactCodeChurn> Churn { get; set; }

        public DimFile()
        {        
            this.Churn = new List<FactCodeChurn>();
        }

        public DimFile(string fileName) : this()
        {
            this.FileExtension = System.IO.Path.GetExtension(fileName);         
        }
    }
}
