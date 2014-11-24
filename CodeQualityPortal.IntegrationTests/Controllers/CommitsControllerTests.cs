using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using NUnit.Framework;

using CodeQualityPortal.ViewModels;


namespace CodeQualityPortal.IntegrationTests.Controllers
{
    [TestFixture]
    public class CommitsControllerTests : WebApiBaseTest
    {
        [Test]
        [TestCase("cs")]
        [TestCase("")]
        public void CommitController_Get(string fileExtension)
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
            resultString = _client.GetStringAsync(MakeUri(string.Format("trend/{0}/{1:s}/{2:s}/{3}", repoId, dateFrom, dateTo, fileExtension))).Result;
            List<CodeChurnByDate> codeChurnByDate = JsonConvert.DeserializeObject<List<CodeChurnByDate>>(resultString);
            Assert.IsNotNull(codeChurnByDate);
            var codeChurnEntry = codeChurnByDate.FirstOrDefault(s => s.DateId.HasValue);
            if (codeChurnEntry == null)
            {
                Assert.Inconclusive("No commit data");
            }
            int dateId = codeChurnEntry.DateId.Value;

            // Act
            resultString = _client.GetStringAsync(MakeUri(string.Format("commits/{0}/{1}/{2}", repoId, dateId, fileExtension))).Result;
            List<CommitCodeChurn> result = JsonConvert.DeserializeObject<List<CommitCodeChurn>>(resultString);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
