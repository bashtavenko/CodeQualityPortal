using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;
using Dapper;
using NUnit.Framework;

namespace CodeQualityPortal.IntegrationTests.Data
{
    public class SummaryRepositoryDoNotDropDbTests
    {
        ISummaryRepository _repository;
        MaintenanceRepository _maintenanceRepository;
        private IDbConnection _db;

        [TestFixtureSetUp]
        public void Setup()
        {
            AutoMapperConfig.CreateMaps();
            _repository = new SummaryRepository();
            _maintenanceRepository = new MaintenanceRepository();
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeQuality"].ConnectionString);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _repository.Dispose();
            _db.Close();
        }

        [Test]
        public void SummaryRepository_GetSystemsByDate()
        {
            // Arrange
            // On which date there are most system data?
            var sql = @"select top 1 i.DateId, count(*) as SystemsCount from (
                        select fm.DateId, ds.Name
                        from DimSystem ds
                        join DimSystemModule dsm on dsm.SystemId = ds.SystemId
                        join DimModule dm on dm.ModuleId = dsm.ModuleId
                        join FactMetrics fm on fm.ModuleId = dm.ModuleId
                        where fm.BranchId is null and fm.MemberId is null and fm.TypeId is null and fm.NamespaceId is null
                        group by fm.DateId, ds.Name ) as i
                        group by i.DateId
                        order by count(*)";
            var date = _db.Query<dynamic>(sql).SingleOrDefault();
            if (date == null)
            {
                Assert.Inconclusive("No data");
            }
            int dateId = date.DateId;
            int systemsCount = date.SystemsCount;

            // Act
            var items = _repository.GetSystemsByDate(dateId);

            // Assert
            Assert.That(items.Count, Is.EqualTo(systemsCount));
        }

        [Test]
        public void GetCoverageBySystems()
        {
            var items = _repository.GetCoverageSummary(90, Category.Systems);
        }

        [Test]
        public void GetCoverageByTeams()
        {
            var items = _repository.GetCoverageSummary(90, Category.Teams);
        }

        [Test]
        public void GetCoverageByOneTeam()
        {
            // Pick a team
            var teamId = _maintenanceRepository.GetIdNames(Category.Teams).First().Id;
            CodeCoverageItem codeCoverage = _repository.GetCoverageSummary(90, Category.Teams).Items.First();

            // Pick a data point
            var dateId =  codeCoverage.DataPoints.First().DateId;

            // Get modulestats summary for this team at this date
            ModuleStatsSummary summary = _repository.GetModuleStatsByCategoryAndDate(Category.Teams, teamId, dateId);
        }

        [Test]
        public void SummaryRepository_GetDatePoints()
        {
            var items = _repository.GetDatePoints();
            Assert.IsNotEmpty(items);
        }
    }
}