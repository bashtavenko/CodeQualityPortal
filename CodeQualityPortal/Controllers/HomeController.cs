using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

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

        [OutputCache(CacheProfile = "HomePage")]
        public ActionResult Index()
        {
            const int topX = 5;
            const int days = 7;
            var dateFrom = DateTime.Now.AddDays(days * -1);
            var dateTo = DateTime.Now;
            var keyStats = _metricsRepository.GetLatestKeyStats();
            var churnTopWorst = _codeChurnRepository.GetWorst(dateFrom, dateTo, topX);
            var membersTopWorst = _metricsRepository.GetWorst(dateFrom, dateTo, topX);
            return View(new HomePage(keyStats, churnTopWorst, membersTopWorst));
        }

        public ActionResult Churn()
        {
            return View("Churn", "");
        }

        public ActionResult Metrics()
        {
            var systems = _metricsRepository.GetSystems();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(systems, Formatting.None, settings);
            return View("Metrics", "", json);
        }

        public ActionResult Systems()
        {
            var latestSystemStats = _metricsRepository.GetLatestSystemStats();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(latestSystemStats, Formatting.None, settings);
            return View("Systems", "", json);
        }

        public ActionResult Reset()
        {
            Response.RemoveOutputCacheItem(Url.Action("Index"));
            return RedirectToAction("index");
        }

        public ActionResult Scatter()
        {
            var datePoints = _metricsRepository.GetDatePoints();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(datePoints, Formatting.None, settings);
            return View("Scatter", "", json);
        }
    }
}