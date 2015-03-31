using System.Linq;
using CodeQualityPortal.Data;
using NUnit.Framework;

namespace CodeQualityPortal.IntegrationTests.Data
{
    [TestFixture]
    public class SeederTest
    {
        private CodeQualityContext _context;

        [TestFixtureSetUp]
        public void Setup()
        {
            _context = new CodeQualityContext(new CodeQualityDropCreateDatabaseAlways());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Seed()
        {
            // Need to get data in order for seeder to work.
            var modules = _context.Modules.ToList();
            Assert.That(modules.Count(), Is.GreaterThan(0));
        }
    }
}
