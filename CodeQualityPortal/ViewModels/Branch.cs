using System;

namespace CodeQualityPortal.ViewModels
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string NameAndDate => $"{Name} ({CreatedDate:MM/dd/yy})";
    }
}