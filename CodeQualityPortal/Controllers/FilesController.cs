using System.Collections.Generic;
using System.Web.Http;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        private readonly CodeChurnRepository _repository;
                
        public FilesController()
        {
            _repository = new CodeChurnRepository();
        }

        [Route("{repoId:int}/{dateId:int}/{fileExtension?}")]
        public IList<FileCodeChurn> Get(int repoId, int dateId, string fileExtension = null)
        {
            return _repository.GetFilesByDate(repoId, dateId, fileExtension != null ? "." + fileExtension : null);
        }

        [Route("{commitId:int}/{fileExtension?}")]
        public IList<FileCodeChurn> Get(int commitId, string fileExtension = null)
        {
            return _repository.GetFilesByCommit(commitId, fileExtension != null ? "." + fileExtension : null);
        }
    }
}
