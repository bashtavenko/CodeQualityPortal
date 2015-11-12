using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CodeQualityPortal.Data;
using Dapper;
using NUnit.Framework;

namespace CodeQualityPortal.IntegrationTests.Data
{
    public class BranchRepositoryDoNotDropDbTests
    {
        IBranchRepository _repository;
        private IDbConnection _db;

        [TestFixtureSetUp]
        public void Setup()
        {
            AutoMapperConfig.CreateMaps();
            _repository = new BranchRepository();
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeQuality"].ConnectionString);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _repository.Dispose();
            _db.Close();
        }


        [Test]
        public void BranchRepository_GetBranchDif()
        {
            // Arrange
            var sql = @"select top 1 fm.BranchId, max(fm.DateId) as DateId
                        from FactMetrics fm
                        join DimBranch db on db.BranchId = fm.BranchId
                        group by fm.BranchId";
            var branch = _db.Query<dynamic>(sql).SingleOrDefault();
            if (branch == null)
            {
                Assert.Inconclusive("No branches to test with");
            }
            int branchId = branch.BranchId;
            int branchDateId = branch.DateId;

            sql = @"select top 1 fm.DateId
                    from FactMetrics fm
                    where fm.BranchId is null";
            var date = _db.Query<dynamic>(sql).Single();
            int masterDateId = date.DateId;

            // Act
            var items = _repository.GetBranchDiff(null, masterDateId, branchId, branchDateId);
        }
    }
}