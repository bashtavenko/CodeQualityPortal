using System.Collections.Generic;
using System.Web.Http;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;
using System;

namespace CodeQualityPortal.Controllers
{
    public class TrendController : ApiController
    {
        private readonly CodeChurnRepository _repository;

        public TrendController()
        {
            _repository = new CodeChurnRepository();
        }
                
        [Route("api/trend/{repoId}/{dateFrom}/{dateTo}/{fileExtension?}")]
        public IList<CodeChurnByDate> Get(int repoId, DateTime dateFrom, DateTime dateTo, string fileExtension = null)
        {
            return _repository.GetTrend(repoId, dateFrom, dateTo, fileExtension != null ? "." + fileExtension : null);
        }     
    }
}
