using System;
using System.Collections.Generic;
using System.Web.Http;

using CodeQualityPortal.Data;
using CodeQualityPortal.ViewModels;

namespace CodeQualityPortal.Controllers
{
    [RoutePrefix("api")]
    public class MetricsController : ApiController
    {
        private readonly IMetricsRepository _repository;

        public MetricsController(IMetricsRepository repository)
        {
            _repository = repository;
        }

        [Route("tags")]
        public IList<string> Get()
        {
            return _repository.GetTags();
        }

        [Route("moduletrend/{tag}/{dateFrom}/{dateTo}")]
        [HttpGet]
        public IList<TrendItem> Get(string tag, DateTime dateFrom, DateTime dateTo)
        {
            return _repository.GetModuleTrend(tag, dateFrom, dateTo);
        }

        [Route("modules/{tag}/{dateId:int}")]
        [HttpGet]
        public IList<ModuleItem> Get(string tag, int dateId)
        {
            return _repository.GetModules(tag, dateId);
        }     
    }
}
