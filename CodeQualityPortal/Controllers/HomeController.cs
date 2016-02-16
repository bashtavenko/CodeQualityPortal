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
        private readonly IBranchRepository _branchRepository;
        private readonly ISummaryRepository _summaryRepository;

        public HomeController(ICodeChurnRepository repository, IMetricsRepository metricsRepository, IBranchRepository branchRepository, ISummaryRepository summaryRepository)
        {
            _codeChurnRepository = repository;
            _metricsRepository = metricsRepository;
            _branchRepository = branchRepository;
            _summaryRepository = summaryRepository;
        }

        [OutputCache(CacheProfile = "HomePage")]
        public ActionResult Index()
        {
            const int topX = 5;
            const int days = 7;
            var dateFrom = DateTime.Now.AddDays(days * -1);
            var dateTo = DateTime.Now;
            var keyStats = _summaryRepository.GetLatestKeyStats();
            var churnTopWorst = _codeChurnRepository.GetWorst(dateFrom, dateTo, topX);
            var membersTopWorst = _summaryRepository.GetWorst(dateFrom, dateTo, topX);
            return View(new HomePage(keyStats, churnTopWorst, membersTopWorst));
        }

        public ActionResult Churn()
        {
            return View("Churn", "");
        }

        public ActionResult Metrics()
        {
            var systems = _metricsRepository.GetSystems();
            var branches = _branchRepository.GetBranches();
            var data = new {Systems = systems, Branches = branches};
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(data, Formatting.None, settings);
            return View("Metrics", "", json);
        }

        public ActionResult Systems()
        {
            var latestSystemStats = _summaryRepository.GetLatestSystemStats();
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
            var datePoints = _summaryRepository.GetDatePoints();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(datePoints, Formatting.None, settings);
            return View((object)json);
        }

        public ActionResult BranchDiff()
        {
            var branches = _branchRepository.GetBranches();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(branches, Formatting.None, settings);
            return View((object)json);
        }

        public ActionResult Coverage()
        {
            var data = _summaryRepository.GetCoverageBySystems(90);
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(data, Formatting.None, settings);
            return View((object)json);
        }
    }
}