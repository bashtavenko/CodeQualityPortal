using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using CodeQualityPortal.Data;

namespace CodeQualityPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICodeChurnRepository _codeChurnRepository;
        private readonly IMetricsRepository _metricsRepository;

        public HomeController(ICodeChurnRepository repository, IMetricsRepository metricsRepository)
        {
            _codeChurnRepository = repository;
            _metricsRepository = metricsRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Churn()
        {
            var repos = _codeChurnRepository.GetRepos();            
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(repos, Formatting.None, settings);
            return View("Churn", "", json);
        }

        public ActionResult Metrics()
        {
            var repos = _metricsRepository.GetTags();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(repos, Formatting.None, settings);
            return View("Metrics", "", json);
        }        
    }
}