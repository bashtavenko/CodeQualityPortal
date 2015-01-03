using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using NUnit.Framework;

using CodeQualityPortal.ViewModels;


namespace CodeQualityPortal.IntegrationTests
{
    [TestFixture]
    public class TrendControllerTests : WebApiBaseTest
    {
        [Test]
        [TestCase("cs")]
        [TestCase("")]
        public void TrendController_Get(string fileExtension)
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

            // Act                        
            if (string.IsNullOrEmpty(fileExtension))
            {
                resultString = _client.GetStringAsync(MakeUri(string.Format("trend/{0}/{1:s}/{2:s}", repoId, dateFrom, dateTo))).Result;
            }
            else
            {
                resultString = _client.GetStringAsync(MakeUri(string.Format("trend/{0}/{1:s}/{2:s}/{3}", repoId, dateFrom, dateTo,fileExtension))).Result;
            }
            var result = JsonConvert.DeserializeObject<List<CodeChurnByDate>>(resultString);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
