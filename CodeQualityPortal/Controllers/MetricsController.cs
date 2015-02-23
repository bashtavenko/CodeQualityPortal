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

        // 1 - tags
        [Route("tags")]
        public IList<string> Get()
        {
            return _repository.GetTags();
        }

        [Route("moduletrend/{tag}/{dateFrom}/{dateTo}")]
        [HttpGet]
        public IList<TrendItem> GetModuleTrend(string tag, DateTime dateFrom, DateTime dateTo)
        {
            return _repository.GetModuleTrend(tag, dateFrom, dateTo);
        }

        [Route("modules/{tag}/{dateId:int}")]
        [HttpGet]
        public IList<ModuleItem> GetModules(string tag, int dateId)
        {
            return _repository.GetModules(tag, dateId);
        }

        // 2 - modules
        [Route("modules/{tag}")]
        [HttpGet]
        public IList<Module> GetModulesByTag(string tag)
        {
            return _repository.GetModulesByTag(tag);
        }

        [Route("namespacetrend/{moduleId}/{dateFrom}/{dateTo}")]
        [HttpGet]
        public IList<TrendItem> GetNamespace(int moduleId, DateTime dateFrom, DateTime dateTo)
        {
            return _repository.GetNamespaceTrend(moduleId, dateFrom, dateTo);
        }

        [Route("namespaces/{moduleId}/{dateId:int}")]
        [HttpGet]
        public IList<NamespaceItem> Get(int moduleId, int dateId)
        {
            return _repository.GetNamespaces(moduleId, dateId);
        }

        // 3 - namespaces
        [Route("namespaces/{moduleId}")]
        [HttpGet]
        public IList<Namespace> GetNamespacesByModule(int moduleId)
        {
            return _repository.GetNamespacesByModule(moduleId);
        }

        [Route("typetrend/{moduleId}/{namespaceId}/{dateFrom}/{dateTo}")]
        [HttpGet]
        public IList<TrendItem> GetTypeTrend(int moduleId, int namespaceId, DateTime dateFrom, DateTime dateTo)
        {
            return _repository.GetTypeTrend(moduleId, namespaceId, dateFrom, dateTo);
        }

        [Route("types/{moduleId}/{namespaceId}/{dateId:int}")]
        [HttpGet]
        public IList<TypeItem> GetTypes(int moduleId, int namespaceId, int dateId)
        {
            return _repository.GetTypes(moduleId, namespaceId, dateId);
        }

        // 4 - types
        [Route("types/{moduleId}/{namespaceId}")]
        [HttpGet]
        public IList<ViewModels.Type> GetTypesByNamespace(int moduleId, int namespaceId)
        {
            return _repository.GetTypesByNamespace(moduleId, namespaceId);
        }

        [Route("membertrend/{moduleId}/{namespaceId}/{typeId}/{dateFrom}/{dateTo}")]
        [HttpGet]
        public IList<TrendItem> GetMemberTrend(int moduleId, int namespaceId, int typeId, DateTime dateFrom, DateTime dateTo)
        {
            return _repository.GetMemberTrend(moduleId, namespaceId, typeId, dateFrom, dateTo);
        }

        [Route("members/{moduleId}/{namespaceId}/{typeId}/{dateId:int}")]
        [HttpGet]
        public IList<MemberItem> GetMembers(int moduleId, int namespaceId, int typeId, int dateId)
        {
            return _repository.GetMembers(moduleId, namespaceId, typeId, dateId);
        }

        // 5 - members
        [Route("members/{moduleId}/{namespaceId}/{typeId}")]
        [HttpGet]
        public IList<Member> GetMembersByType(int moduleId, int namespaceId, int typeId)
        {
            return _repository.GetMembersByType(moduleId, namespaceId, typeId);
        }

        [Route("singlemembertrend/{moduleId}/{namespaceId}/{typeId}/{memberId}/{dateFrom}/{dateTo}")]
        [HttpGet]
        public IList<TrendItem> GetSingleMemberTrend(int moduleId, int namespaceId, int typeId,  int memberId, DateTime dateFrom, DateTime dateTo)
        {
            return _repository.GetSingleMemberTrend(moduleId, namespaceId, typeId, memberId, dateFrom, dateTo);
        }
    }
}
