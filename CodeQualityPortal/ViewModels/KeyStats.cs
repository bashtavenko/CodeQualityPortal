using System;
using System.Collections.Generic;
using System.Linq;
using CodeQualityPortal.Data;

namespace CodeQualityPortal.ViewModels
{
    public class KeyStats
    {
        public DateTime? Date { get; set; }
        public int? MaintainabilityIndex { get; set; }
        public int? CodeCoverage { get; set; }
        public int? LinesOfCode { get; set; }
        public int? CyclomaticComplexity { get; set; }
        
        public KeyStats(DateTime date, IList<FactMetrics> modules)
        {
            Date = date;
            MaintainabilityIndex = Convert.ToInt32(modules.Average(a => a.MaintainabilityIndex));
            CodeCoverage = Convert.ToInt32(modules.Average(a => a.CodeCoverage));
            CyclomaticComplexity = Convert.ToInt32(modules.Sum(a => a.CyclomaticComplexity));
            LinesOfCode = Convert.ToInt32(modules.Sum(a => a.LinesOfCode));
        }
    }
}