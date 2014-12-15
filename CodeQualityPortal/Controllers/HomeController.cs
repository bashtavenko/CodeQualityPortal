using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using CodeQualityPortal.Data;

namespace CodeQualityPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICodeChurnRepository _repository;

        public HomeController(ICodeChurnRepository repository)
        {
            _repository = repository;
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