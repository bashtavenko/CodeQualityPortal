using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeQualityPortal.ViewModels
{
    public class DateItem
    {
        public DateTime Date { get; set; }
        public int? DateId { get; set; }
        public string DateString { get { return Date.ToString("MM/dd"); } }
    }
}