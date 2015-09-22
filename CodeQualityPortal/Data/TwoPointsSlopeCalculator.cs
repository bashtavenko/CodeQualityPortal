using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.Data
{
    public class TwoPointsSlopeCalculator : SlopeCalculator
    {
        public TwoPointsSlopeCalculator(List<DataPoint> dataPoints) : base(dataPoints)
        {
        }

        public override void CalculateSlope()
        {
            if (DataPoints.Count <= 1)
            {
                Slope = 0;
                return;
            }

            var twoPoints = DataPoints
                .OrderBy(o => o.Date)
                .Select(s => s.Value)
                .ToList();

            int x = twoPoints.First();
            int y = twoPoints.Last();

            if (y == x)
            {
                Slope = 0;
            }
            else if (y > x)
            {
                Slope = 45;
            }
            else
            {
                Slope = -45;
            }
        }
    }
}