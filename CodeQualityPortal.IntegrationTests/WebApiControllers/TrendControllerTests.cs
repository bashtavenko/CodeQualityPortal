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
            var resultString = _client.GetStringAsync(MakeUri(string.Format("trend/{0:s}/{1:s}", dateFrom, dateTo))).Result;
            
            
            var result = JsonConvert.DeserializeObject<List<CodeChurnByDate>>(resultString);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
