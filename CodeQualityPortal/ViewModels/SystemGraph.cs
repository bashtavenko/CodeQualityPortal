using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.ViewModels
{
    public class SystemGraph
    {
        public int SystemId { get; set; }
        public string SystemName { get; set; }
        public IList<DataPoint> DataPoints { get; set; }
        public int AverageValue
        {
            get { return (int) DataPoints.Average(x => x.Value); }
        }
    }
}