using System;

namespace CodeQualityPortal.ViewModels
{
    public class RepoCodeChurnSummary
    {
        public int RepoId { get; set; }
        public string RepoName { get; set; }
        public int CommitCount { get; set; }
        public int LinesAdded { get; set; }        
        public int LinesDeleted { get; set; }
        public int TotalChurn { get { return LinesAdded + LinesDeleted; }}
    }
}