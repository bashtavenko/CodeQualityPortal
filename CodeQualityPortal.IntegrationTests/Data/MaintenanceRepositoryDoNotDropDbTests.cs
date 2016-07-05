using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CodeQualityPortal.Data;
using NUnit.Framework;

namespace CodeQualityPortal.IntegrationTests.Data
{
    public class MaintenanceRepositoryDoNotDropDbTests
    {
        MaintenanceRepository _repository;
        private IDbConnection _db;

        [TestFixtureSetUp]
        public void Setup()
        {
            AutoMapperConfig.CreateMaps();
            _repository = new MaintenanceRepository();
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeQuality"].ConnectionString);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _repository.Dispose();
            _db.Close();
        }

        [Test]
        public void GetIdNames()
        {
            var items = _repository.GetIdNames(Category.Systems);
            items = _repository.GetIdNames(Category.Repos);
            items = _repository.GetIdNames(Category.Teams);
        }

        [Test]
        public void ModuleLookup()
        {
            var idName = _repository.GetIdNames(Category.Systems).FirstOrDefault();
            if (idName == null) Assert.Inconclusive();

            var modules = _repository.GetModulesByCategory(Category.Teams, idName.Id);
            modules = _repository.GetModulesByCategory(Category.Repos, null);
            modules = _repository.GetModulesByCategory(Category.Systems, null);
        }
    }
}