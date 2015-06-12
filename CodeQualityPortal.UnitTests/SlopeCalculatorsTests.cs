using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using CodeQualityPortal.Data;
using NUnit.Framework;

namespace CodeQualityPortal.UnitTests
{
    [TestFixture]
    public class SlopeCalculatorsTests
    {
        [Test]
        public void TwoPointCalculator_Basic()
        {
            // Arrange
            var dataPoints = new List<DataPoint>
            {
                new DataPoint { Date = new DateTime(2015, 06, 12), Value = 20},
                new DataPoint { Date = new DateTime(2015, 06, 10), Value = 5}
            };

            SlopeCalculator calc = new TwoPointsSlopeCalculator(dataPoints);

            // Act
            calc.CalculateSlope();

            // Assert
            Assert.That(calc.Slope, Is.EqualTo(45m));
            Assert.That(calc.TrendIndicator, Is.EqualTo(TrendIndicator.Ascends));
        }

        [Test]
        public void TwoPointCalculator_Basic_2()
        {
            // Arrange
            var dataPoints = new List<DataPoint>
            {
                new DataPoint { Date = new DateTime(2015, 06, 12), Value = 5},
                new DataPoint { Date = new DateTime(2015, 06, 10), Value = 20}
            };

            SlopeCalculator calc = new TwoPointsSlopeCalculator(dataPoints);

            // Act
            calc.CalculateSlope();

            // Assert
            Assert.That(calc.Slope, Is.EqualTo(-45m));
            Assert.That(calc.TrendIndicator, Is.EqualTo(TrendIndicator.Descends));
        }
    }
}
