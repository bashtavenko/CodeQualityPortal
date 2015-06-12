using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.Data
{
    public class Trend
    {
        private readonly SlopeCalculator _slopeCalculator;

        public DataPoint LastDataPoint
        {
            get { return DataPoints.OrderByDescending(s => s.Date).FirstOrDefault(); }
        }
        public List<DataPoint> DataPoints { get; private set; }
        public decimal Slope { get { return _slopeCalculator.Slope; } }
        public TrendIndicator TrendIndicator { get { return _slopeCalculator.TrendIndicator; } }

        public Trend() : this(new List<DataPoint>())
        {
        }

        public Trend(List<DataPoint> dataPoints)
            : this(dataPoints, new TwoPointsSlopeCalculator(dataPoints))
        {
        }

        public Trend(List<DataPoint> dataPoints, SlopeCalculator slopeCalculator)
        {
            _slopeCalculator = slopeCalculator;
            DataPoints = dataPoints;
        }

        public void CalculateSlope()
        {
            _slopeCalculator.CalculateSlope();
        }
    }
}