using CodeQualityPortal.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQualityPortal.IntegrationTests.Data
{
    [TestFixture]
    public class RepositoryTests
    {
        ICodeChurnRepository _repository;

        [TestFixtureSetUp]
        public void Setup()
        {
            _repository = new CodeChurnRepository();
        }

        [Test]
        public void Repository_GetRepos_CanGet()
        {
            var result = _repository.GetRepos();
            Assert.IsTrue(result.Count() > 0);            
        }

        [Test]
        public void Repository_GetCodeChurn_WithExtension()
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
            var result = _repository.GetCodeChurnTrend(repoId, dateTo.AddMonths(-1), dateTo, fileExtension);            
            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public void Repository_GetCodeChurn_WithoutExtension()
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
            var result = _repository.GetCodeChurnTrend(repoId, dateTo.AddMonths(-1), dateTo, null);
            Assert.IsTrue(result.Count() > 0);
        }
    }
}
