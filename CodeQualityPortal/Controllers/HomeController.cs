using CodeQualityPortal.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeQualityPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly CodeChurnRepository _repository;

        public HomeController()
        {
            _repository = new CodeChurnRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Churn()
        {
            var repos = _repository.GetRepos();            
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(repos, Formatting.None, settings);
            return View("Churn", "", json);
        }        
    }
}