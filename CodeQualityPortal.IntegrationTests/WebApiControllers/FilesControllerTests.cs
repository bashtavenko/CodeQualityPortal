using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using NUnit.Framework;

using CodeQualityPortal.ViewModels;


namespace CodeQualityPortal.IntegrationTests
{
    [TestFixture]
    public class FilesControllerTests : WebApiBaseTest
    {
        [Test]
        [TestCase("cs", null)]
        [TestCase("", null)]
        [TestCase("cs", 5)]
        public void FilesController_Get_ByDate(string fileExtension, int? topX)
        {
            // Arrange
            var resultString = _client.GetStringAsync(MakeUri("repos")).Result;
            var repos = JsonConvert.DeserializeObject<List<Repo>>(resultString);
            Assert.IsNotNull(repos);
            if (repos.Count == 0)
            {
                Assert.Inconclusive("No data");
            }
            int repoId = repos[0].RepoId;
            DateTime dateTo = DateTime.Now;
            DateTime dateFrom = dateTo.AddYears(-1);
            string url = MakeUri(string.Format("trend/{0}/{1:s}/{2:s}/{3}", repoId, dateFrom, dateTo, fileExtension));
            resultString = _client.GetStringAsync(url).Result;
            List<CodeChurnByDate> codeChurnByDate = JsonConvert.DeserializeObject<List<CodeChurnByDate>>(resultString);
            Assert.IsNotNull(codeChurnByDate);
            var codeChurnEntry = codeChurnByDate.FirstOrDefault(s => s.DateId.HasValue);
            if (codeChurnEntry == null)
            {
                Assert.Inconclusive("No commit data");
            }
            int dateId = codeChurnEntry.DateId.Value;            

            // Act
            url = MakeUri(string.Format("Files/{0}/{1}/{2}?topX={3}", repoId, dateId, fileExtension, topX));
            resultString = _client.GetStringAsync(url).Result;
            List<CommitCodeChurn> result = JsonConvert.DeserializeObject<List<CommitCodeChurn>>(resultString);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        [TestCase("cs")]
        [TestCase("")]
        public void FilesController_Get_ByCommit(string fileExtension)
        {
            // Arrange
            var resultString = _client.GetStringAsync(MakeUri("repos")).Result;
            var repos = JsonConvert.DeserializeObject<List<Repo>>(resultString);
            Assert.IsNotNull(repos);
            if (repos.Count == 0)
            {
                Assert.Inconclusive("No data");
            }
            int repoId = repos[0].RepoId;
            DateTime dateTo = DateTime.Now;
            DateTime dateFrom = dateTo.AddYears(-1);
            string url = MakeUri(string.Format("trend/{0}/{1:s}/{2:s}/{3}", repoId, dateFrom, dateTo, fileExtension));
            resultString = _client.GetStringAsync(url).Result;
            List<CodeChurnByDate> codeChurnByDate = JsonConvert.DeserializeObject<List<CodeChurnByDate>>(resultString);
            Assert.IsNotNull(codeChurnByDate);
            var codeChurnEntry = codeChurnByDate.FirstOrDefault(s => s.DateId.HasValue);
            if (codeChurnEntry == null)
            {
                Assert.Inconclusive("No trend data");
            }
            int dateId = codeChurnEntry.DateId.Value;
                        
            // Act
            url = MakeUri(string.Format("Files/{0}/{1}/{2}", repoId, dateId, fileExtension));
            resultString = _client.GetStringAsync(url).Result;
            List<CommitCodeChurn> result = JsonConvert.DeserializeObject<List<CommitCodeChurn>>(resultString);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
