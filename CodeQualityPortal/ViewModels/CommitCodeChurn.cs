using System;

namespace CodeQualityPortal.ViewModels
{
    public class CommitCodeChurn
    {
        public int CommitId { get; set; }
        public string Sha { get; set; }
        public string Url { get; set; }
        public string Committer { get; set; }
        public string CommitterAvatarUrl { get; set; }
        public string Message { get; set; }        
        public int LinesAdded { get; set; }        
        public int LinesDeleted { get; set; }
        public int TotalChurn { get; set; }
    }
}