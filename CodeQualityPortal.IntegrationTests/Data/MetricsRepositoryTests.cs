using System;
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
            _repository = new MetricsRepository(new CodeQualityDropCreateDatabaseAlways());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _repository.Dispose();
        }
        
        [Test]
        public void MetricsRepository_GetSystems()
        {
            var result = _repository.GetSystems();
            Assert.IsTrue(result.Any());            
        }

        [Test]
        public void MetricsRepository_GetModuleTrend_WithSystem()
        {
            // Arrange
            DimSystem system; 
            DateTime dateTo;
            int? branchId;
            using (var ctx = new CodeQualityContext())
            {
                var item = ctx.Systems
                    .SelectMany(s => s.Modules, (s, m) => new {System = s, Module = m}) // Module because we need to pass it on to the next SelectMany
                    .SelectMany(d => d.Module.Metrics, (d, f) => new {System = d.System, Fact = f})
                    .FirstOrDefault();

                if (item == null)
                {
                    Assert.Inconclusive("No data");
                }
                system = item.System;
                dateTo = item.Fact.Date.Date;
                branchId = item.Fact.BranchId;
            }

            // Act
            var result = _repository.GetModuleTrend(branchId, system.SystemId, dateTo.AddDays(-7), dateTo);
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void MetricsRepository_GetModuleTrend_WithoutSystem()
        {
            // Arrange
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
                dateTo = item.Date.DateTime;
            }

            // Act
            var result = _repository.GetModuleTrend(null, null, dateTo.AddDays(-7), dateTo);
            Assert.IsTrue(result.Any());
        }


        [Test]
        public void MetricsRepository_GetNamespacesByModule()
        {
            // Arrange
            int moduleId;
            using (var ctx = new CodeQualityContext())
            {
                var item = ctx.Metrics
                    .Where(w => w.Module != null)
                    .OrderByDescending(d => d.Date.DateTime).FirstOrDefault();
                if (item == null)
                {
                    Assert.Inconclusive("No data");
                }
                moduleId = item.ModuleId.Value;
            }

            // Act
            var result = _repository.GetNamespacesByModule(moduleId);
            Assert.IsTrue(result.Any());
        }


        [Test]
        public void MetricsRepository_GetTypesByNamespace()
        {
            // Arrange
            int moduleId;
            int namespaceId;
            using (var ctx = new CodeQualityContext())
            {
                var item = ctx.Metrics
                    .Where(w => w.Module != null && w.NamespaceId != null)
                    .OrderByDescending(d => d.Date.DateTime).FirstOrDefault();
                if (item == null)
                {
                    Assert.Inconclusive("No data");
                }
                moduleId = item.ModuleId.Value;
                namespaceId = item.NamespaceId.Value;
            }

            // Act
            var result = _repository.GetTypesByNamespace(moduleId, namespaceId);
            Assert.IsTrue(result.Any());
        }

        //[Test]
        public void MetricsRepository_GetModuleSystems()
        {
            // Arrange
            DimSystem system;
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
                system = item.Module.Systems.First();
                dateId = item.DateId;
            }

            // Act
            var result = _repository.GetModules(null, system.SystemId, dateId);
            Assert.IsTrue(result.Any());
        }
    }
}
