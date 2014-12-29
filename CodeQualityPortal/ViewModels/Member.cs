using System;

namespace CodeQualityPortal.ViewModels
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public int? Line { get; set; }
    }
}