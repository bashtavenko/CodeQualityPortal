namespace CodeQualityPortal.ViewModels
{
    public class FileCodeChurn
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public int LinesAdded { get; set; }
        public int LinesModified { get; set; }
        public int LinesDeleted { get; set; }
        public int TotalChurn { get; set; }
    }
}