using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using NUnit.Framework;

using CodeQualityPortal.ViewModels;


namespace CodeQualityPortal.IntegrationTests.Controllers
{
    [TestFixture]
    public class WebApiBaseTest
    {
        protected HttpClient _client;
        private HttpServer _server;

        [TestFixtureSetUp]
        protected void Setup()
        {         
            NinjectWebCommon.Start();

            var config = new HttpConfiguration();
            config.DependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;            
            WebApiConfig.Register(config);
            _server = new HttpServer(config);            
            _client = new HttpClient(_server);    
        }
        [TestFixtureTearDown]
        protected void TearDown()
        {
            _server.Dispose();
            NinjectWebCommon.Stop();
        }        

        protected string MakeUri(string uri)
        {
            return string.Format("http://localhost:5000/api/{0}", uri);
        }
    }
}
