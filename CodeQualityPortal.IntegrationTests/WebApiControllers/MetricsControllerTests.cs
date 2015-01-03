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
    public class MetricsControllerTests : WebApiBaseTest
    {
        [Test]        
        public void MetricsController_GetTags()
        {            
            // Act                                    
            var resultString = _client.GetStringAsync(MakeUri("tags")).Result;
            var result = JsonConvert.DeserializeObject<List<string>>(resultString);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
