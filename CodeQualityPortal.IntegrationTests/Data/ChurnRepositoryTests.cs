using System;
using System.IO;
using System.Linq;

using CodeQualityPortal.Data;
using NUnit.Framework;

namespace CodeQualityPortal.IntegrationTests.Data
{
    [TestFixture]
    public class ChurnRepositoryTests
    {
        ICodeChurnRepository _repository;

        [TestFixtureSetUp]
        public void Setup()
        {
            AutoMapperConfig.CreateMaps();
            _repository = new CodeChurnRepository(new CodeQualityDropCreateDatabaseAlways());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _repository.Dispose();
        }

        [Test]
        public void ChurnRepository_GetRepos_CanGet()
        {
            var result = _repository.GetRepos();
            Assert.IsTrue(result.Count() > 0);            
        }

        
        [Test]
        public void ChurnRepository_GetTrend()
        {
            // Arrange
            DateTime dateTo;            
            using (var ctx = new CodeQualityContext())
            {
                var lastChurn = ctx.Churn.Where(w => w.File != null).OrderByDescending(o => o.Date.Date).FirstOrDefault();
                if (lastChurn == null)
                {
                    Assert.Inconclusive("No data");
                }
                dateTo = lastChurn.Date.Date;
            }

            // Act
            var result = _repository.GetTrend(dateTo.AddMonths(-1), dateTo);

            // Assert
            Assert.IsTrue(result.Count() > 0);
        }
        

        [Test]
        public void ChurnRepository_GetFilesByDate()
        {
            // Arrange
            int repoId;
            int dateId;
            using (var ctx = new CodeQualityContext())
            {
                var lastChurn = ctx.Churn.Where(w => w.File != null).FirstOrDefault();
                if (lastChurn == null)
                {
                    Assert.Inconclusive("No data");
                }
                repoId = lastChurn.File.Commits.First().RepoId;                
                dateId = lastChurn.Date.DateId;
            }

            // Act
            var result = _repository.GetFilesByDate(repoId, dateId, null);

            // Assert
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ChurnRepository_GetCommitsByDate()
        {
            // Arrange
            int repoId;
            int dateId;            
            using (var ctx = new CodeQualityContext())
            {
                var lastChurn = ctx.Churn.Where(w => w.File != null).FirstOrDefault();
                if (lastChurn == null)
                {
                    Assert.Inconclusive("No data");
                }
                repoId = lastChurn.File.Commits.First().RepoId;
                dateId = lastChurn.Date.DateId;                
            }

            // Act
            var result = _repository.GetCommitsByDate(repoId, dateId);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }
        
        [Test]
        public void ChurnRepository_GetWorst()
        {
            var result = _repository.GetWorst(DateTime.Now.AddYears(-1), DateTime.Now, 5);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ChurnRepository_GetSummary()
        {
            // Arrange
            int dateId;
            using (var ctx = new CodeQualityContext())
            {
                var lastChurn = ctx.Churn.FirstOrDefault(w => w.File == null);
                if (lastChurn == null)
                {
                    Assert.Inconclusive("No data");
                }
                dateId = lastChurn.Date.DateId;
            }

            var result = _repository.GetRepoChurnSummaryByDate(dateId);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
