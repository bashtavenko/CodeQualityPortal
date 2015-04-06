using System.Collections.Generic;
using System.Web.Http;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        private readonly ICodeChurnRepository _repository;
                
        public FilesController(ICodeChurnRepository repository)
        {
            _repository = repository;
        }

        [Route("{repoId:int}/{dateId:int}/{fileExtension?}")]
        public IList<FileCodeChurn> Get(int repoId, int dateId, string fileExtension = null, int? topX = null)
        {
            return _repository.GetFilesByDate(repoId, dateId, topX);
        }

        [Route("{commitId:int}/{fileExtension?}")]
        public IList<FileCodeChurn> Get(int commitId, string fileExtension = null)
        {
            return _repository.GetFilesByCommit(commitId);
        }
    }
}
