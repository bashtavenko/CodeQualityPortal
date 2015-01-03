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
            _repository = new CodeChurnRepository();
        }

        [Test]
        public void ChurnRepository_GetRepos_CanGet()
        {
            var result = _repository.GetRepos();
            Assert.IsTrue(result.Count() > 0);            
        }

        [Test]
        public void ChurnRepository_GetTrend_WithExtension()
        {
            // Arrange
            int repoId;
            DateTime dateTo;
            string fileExtension;
            using (var ctx = new CodeQualityContext())
            {
                var lastChurn = ctx.Churn.Where(w => w.File !=null).OrderByDescending(o => o.Date.Date).FirstOrDefault();
                if (lastChurn == null)
                {
                    Assert.Inconclusive("No data");
                }
                repoId = lastChurn.File.Commit.RepoId;
                fileExtension = lastChurn.File.FileExtension;
                dateTo = lastChurn.Date.Date;
            }

            // Act
            var result = _repository.GetTrend(repoId, dateTo.AddMonths(-1), dateTo, fileExtension);            
            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public void ChurnRepository_GetTrend_WithoutExtension()
        {
            // Arrange
            int repoId;
            DateTime dateTo;            
            using (var ctx = new CodeQualityContext())
            {
                var lastChurn = ctx.Churn.Where(w => w.File != null).OrderByDescending(o => o.Date.Date).FirstOrDefault();
                if (lastChurn == null)
                {
                    Assert.Inconclusive("No data");
                }
                repoId = lastChurn.File.Commit.RepoId;                
                dateTo = lastChurn.Date.Date;
            }

            // Act
            var result = _repository.GetTrend(repoId, dateTo.AddMonths(-1), dateTo, null);

            // Assert
            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public void ChurnRepository_GetTrend_WithBogusExtension()
        {
            // Arrange
            int repoId;
            DateTime dateTo;
            using (var ctx = new CodeQualityContext())
            {
                var lastChurn = ctx.Churn.Where(w => w.File != null).OrderByDescending(o => o.Date.Date).FirstOrDefault();
                if (lastChurn == null)
                {
                    Assert.Inconclusive("No data");
                }
                repoId = lastChurn.File.Commit.RepoId;
                dateTo = lastChurn.Date.Date;
            }

            // Act
            var result = _repository.GetTrend(repoId, dateTo.AddMonths(-1), dateTo, "cs");

            // Assert
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void ChurnRepository_GetFilesByDate_WithoutExtension()
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
                repoId = lastChurn.File.Commit.RepoId;                
                dateId = lastChurn.Date.DateId;
            }

            // Act
            var result = _repository.GetFilesByDate(repoId, dateId, null, null);

            // Assert
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ChurnRepository_GetFilesByDate_WithExtension()
        {
            // Arrange
            int repoId;
            int dateId;
            string fileExtension;
            using (var ctx = new CodeQualityContext())
            {
                var someChurn = ctx.Churn.Where(w => w.File != null).OrderBy(o => o.TotalChurn).FirstOrDefault();
                if (someChurn == null)
                {
                    Assert.Inconclusive("No data");
                }
                repoId = someChurn.File.Commit.RepoId;
                dateId = someChurn.Date.DateId;
                fileExtension = someChurn.File.FileExtension;
            }

            // Act
            var result = _repository.GetFilesByDate(repoId, dateId, fileExtension, null);

            // Assert
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(string.Equals(fileExtension, Path.GetExtension(result.First().FileName), StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void ChurnRepository_GetCommitsByDate_WithExtension()
        {
            // Arrange
            int repoId;
            int dateId;
            string fileExtension;
            using (var ctx = new CodeQualityContext())
            {
                var lastChurn = ctx.Churn.Where(w => w.File != null).FirstOrDefault();
                if (lastChurn == null)
                {
                    Assert.Inconclusive("No data");
                }
                repoId = lastChurn.File.Commit.RepoId;
                dateId = lastChurn.Date.DateId;
                fileExtension = lastChurn.File.FileExtension;
            }

            // Act
            var result = _repository.GetCommitsByDate(repoId, dateId, fileExtension);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void ChurnRepository_GetCommitsByDate_WithoutExtension()
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
                repoId = lastChurn.File.Commit.RepoId;
                dateId = lastChurn.Date.DateId;                
            }

            // Act
            var result = _repository.GetCommitsByDate(repoId, dateId, null);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void ChurnRepository_GetFilesByCommit_WithExtension()
        {
            // Arrange            
            int commitId;
            string fileExtension;
            using (var ctx = new CodeQualityContext())
            {
                var lastChurn = ctx.Churn.Where(w => w.File != null).FirstOrDefault();
                if (lastChurn == null)
                {
                    Assert.Inconclusive("No data");
                }                
                commitId = lastChurn.CommitId;
                fileExtension = lastChurn.File.FileExtension;
            }

            // Act
            var result = _repository.GetFilesByCommit(commitId, fileExtension);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void ChurnRepository_GetWorst()
        {
            var result = _repository.GetWorst(DateTime.Now.AddYears(-1), DateTime.Now, 5);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
