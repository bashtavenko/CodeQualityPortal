using System;
using System.Collections.Generic;
using System.Web.Http;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Controllers
{
    public class TrendController : ApiController
    {
        private readonly ICodeChurnRepository _repository;

        public TrendController(ICodeChurnRepository repository)
        {
            _repository = repository;
        }
                
        [Route("api/trend/{repoId}/{dateFrom}/{dateTo}/{fileExtension?}")]
        public IList<CodeChurnByDate> Get(int repoId, DateTime dateFrom, DateTime dateTo, string fileExtension = null)
        {
            return _repository.GetTrend(repoId, dateFrom, dateTo, fileExtension != null ? "." + fileExtension : null);
        }     
    }
}
