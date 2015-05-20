using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;
using Ninject.Activation.Caching;

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

        public ActionResult Reset()
        {
            Response.RemoveOutputCacheItem(Url.Action("Index"));
            return RedirectToAction("index");
        }
    }
}