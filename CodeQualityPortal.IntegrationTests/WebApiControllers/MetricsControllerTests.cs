using System.Collections.Generic;

using Newtonsoft.Json;
using NUnit.Framework;

namespace CodeQualityPortal.IntegrationTests
{
    [TestFixture]
    public class MetricsControllerTests : WebApiBaseTest
    {
        //[Test]        TODO: GET systems
        public void MetricsController_GetTags()
        {            
            // Act                                    
            var resultString = _client.GetStringAsync(MakeUri("systems")).Result;
            var result = JsonConvert.DeserializeObject<List<string>>(resultString);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
