using System.Collections.Generic;
using System.Web.Http;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Controllers
{
    public class ReposController : ApiController
    {
        private readonly ICodeChurnRepository _repository;

        public ReposController(ICodeChurnRepository repository)
        {
            _repository = repository;
        }

        [Route("api/repos")]
        public IList<Repo> Get()
        {
            return _repository.GetRepos();
        }     
    }
}
