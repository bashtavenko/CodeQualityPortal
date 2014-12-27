using System;
using System.IO;
using System.Linq;

using NUnit.Framework;

using CodeQualityPortal.Data;

namespace CodeQualityPortal.IntegrationTests.Data
{
    [TestFixture]
    public class MetricsRepositoryTests
    {
        IMetricsRepository _repository;

        [TestFixtureSetUp]
        public void Setup()
        {
            AutoMapperConfig.CreateMaps();
            _repository = new MetricsRepository();
        }

        [Test]
        public void MetricsRepository_GetTags()
        {
            var result = _repository.GetTags();
            Assert.IsTrue(result.Count() > 0);            
        }

        [Test]
        public void MetricsRepository_GetModuleTrend()
        {
            // Arrange
            string tag;
            DateTime dateTo;            
            using (var ctx = new CodeQualityContext())
            {
                var item = ctx.Metrics
                    .Where(w => w.Member != null)
                    .OrderByDescending(d => d.Date.DateTime).FirstOrDefault();
                if (item == null)
                {
                    Assert.Inconclusive("No data");
                }
                tag = item.Member.Type.Namespace.Module.Target.Tag;
                dateTo = item.Date.DateTime;
            }

            // Act
            var result = _repository.GetModuleTrend(tag, dateTo.AddDays(-7), dateTo);
            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public void MetricsRepository_GetModules()
        {
            // Arrange
            string tag;
            int dateId;
            using (var ctx = new CodeQualityContext())
            {
                var item = ctx.Metrics
                    .Where(w => w.Member != null)
                    .OrderByDescending(d => d.Date.DateTime).FirstOrDefault();
                if (item == null)
                {
                    Assert.Inconclusive("No data");
                }
                tag = item.Member.Type.Namespace.Module.Target.Tag;
                dateId = item.DateId;
            }

            // Act
            var result = _repository.GetModules(tag, dateId);
            Assert.IsTrue(result.Count() > 0);
        }     
    }
}
