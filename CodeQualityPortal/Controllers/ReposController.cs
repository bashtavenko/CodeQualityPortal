using System.Collections.Generic;
using System.Web.Http;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Controllers
{
    public class ReposController : ApiController
    {
        private readonly CodeChurnRepository _repository;

        public ReposController()
        {
            _repository = new CodeChurnRepository();
        }

        [Route("api/repos")]
        public IList<Repo> Get()
        {
            return _repository.GetRepos();
        }     
    }
}
