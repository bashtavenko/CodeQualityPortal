using System.Collections.Generic;
using System.Web.Http;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Controllers
{
    public class CommitsController : ApiController
    {
        private readonly CodeChurnRepository _repository;

        public CommitsController()
        {
            _repository = new CodeChurnRepository();
        }
                
        [Route("api/commits/{repoId}/{dateId}/{fileExtension?}")]
        public IList<CommitCodeChurn> Get(int repoId, int dateId, string fileExtension = null)
        {
            return _repository.GetCommitsByDate(repoId, dateId, fileExtension != null ? "." + fileExtension : null);
        }     
    }
}
