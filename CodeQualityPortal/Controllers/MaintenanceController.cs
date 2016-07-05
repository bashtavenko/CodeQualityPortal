using System.Collections.Generic;
using System.Web.Http;
using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Controllers
{
    [RoutePrefix("api")]
    public class MaintenanceController : ApiController
    {
        private readonly MaintenanceRepository _repository;

        public MaintenanceController(MaintenanceRepository repository)
        {
            _repository = repository;
        }

        [Route("maintenance/lists/{category}")]
        public IList<IdName> Get(Category category)
        {
            return _repository.GetIdNames(category);
        }

        [Route("maintenance/modules/{category}/{value?}")]
        public IList<Module> GetModules(Category category, int? value = null)
        {
            return _repository.GetModulesByCategory(category, value);
        }
    }
}