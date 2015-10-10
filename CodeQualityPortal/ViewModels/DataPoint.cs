using System;

namespace CodeQualityPortal.ViewModels
{
    public class DataPoint
    {
        public int DateId { get; set; }
        public DateTime Date { get; set; }        
        public string DateString { get { return Date.ToString("MM/dd"); } }
        public int Value { get; set; }
    }
}