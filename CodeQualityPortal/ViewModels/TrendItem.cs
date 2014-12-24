using System;

namespace CodeQualityPortal.ViewModels
{
    public class TrendItem  : MetricsItem
    {        
        public DateTime Date { get; set; }
        public int? DateId { get; set; }
        public string DateString { get { return Date.ToString("MM/dd"); } }
    }
}