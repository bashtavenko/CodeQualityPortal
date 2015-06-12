using System.Collections.Generic;

namespace CodeQualityPortal.Data
{
    public abstract class SlopeCalculator
    {
        public decimal Slope { get; protected set; }

        public TrendIndicator TrendIndicator
        {
            get
            {
                if (Slope == 0)
                {
                    return TrendIndicator.Flat;
                }
                else if (Slope > 0)
                {
                    return TrendIndicator.Ascends;
                }
                else
                {
                    return TrendIndicator.Descends;
                }
            }
        }
        public List<DataPoint> DataPoints { get; set; }
        
        protected SlopeCalculator(List<DataPoint> dataPoints) : this()
        {
            DataPoints = dataPoints;
        }

        protected SlopeCalculator()
        {
            DataPoints = new List<DataPoint>();
        }

        public abstract void CalculateSlope();
    }
}