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
                
        [Route("api/trend/{dateFrom}/{dateTo}")]
        public IList<CodeChurnByDate> Get(DateTime dateFrom, DateTime dateTo)
        {
            return _repository.GetTrend(dateFrom, dateTo);
        }

        [Route("api/reposummary/{dateId}")]
        public IList<RepoCodeChurnSummary> Get(int dateId)
        {
            return _repository.GetRepoChurnSummaryByDate(dateId);
        }     
    }
}
