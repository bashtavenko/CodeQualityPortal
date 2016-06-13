using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.ViewModels
{
    public class CodeCoverageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<DataPoint> DataPoints { get; set; }
        public int AverageValue
        {
            get { return DataPoints.Any() ? (int) DataPoints.Average(x => x.Value) : default(int); }
        }
    }
}