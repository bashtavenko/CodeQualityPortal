using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.ViewModels
{
    /// <summary>
    /// Systems, Teams, Repo are some of the code coverage items. All of them have Id, Name and list 
    /// </summary>
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