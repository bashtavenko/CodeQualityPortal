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
        public void TrendController_Get()
        {
            // Arrange
            DateTime dateTo = DateTime.Now;
            DateTime dateFrom = dateTo.AddYears(-1);

            // Act                        
            var uri = MakeUri($"trend/{dateFrom:s}/{dateTo:s}");
            var resultString = _client.GetStringAsync(uri).Result;
            
            
            var result = JsonConvert.DeserializeObject<List<CodeChurnByDate>>(resultString);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
