using System.Collections.Generic;
using System.Web.Http;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Controllers
{
    public class CommitsController : ApiController
    {
        private readonly ICodeChurnRepository _repository;

        public CommitsController(ICodeChurnRepository repository)
        {
            _repository = repository;
        }
                
        [Route("api/commits/{repoId}/{dateId}/{fileExtension?}")]
        public IList<CommitCodeChurn> Get(int repoId, int dateId)
        {
            return _repository.GetCommitsByDate(repoId, dateId);
        }     
    }
}
