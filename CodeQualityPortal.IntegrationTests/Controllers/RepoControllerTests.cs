using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using NUnit.Framework;

using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.IntegrationTests.Controllers
{
    [TestFixture]
    public class RepoControllerTests : WebApiBaseTest
    {
        [Test]        
        public void RepoController_Get()
        {
            string url = MakeUri("repos");
            var resultString = _client.GetStringAsync(url).Result;
            var response = JsonConvert.DeserializeObject<List<Repo>>(resultString);
        }
    }
}
