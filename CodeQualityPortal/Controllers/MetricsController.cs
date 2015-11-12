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

        // 1 - systems
        [Route("systems")]
        public IList<ViewModels.SystemDefinition> Get()
        {
            return _repository.GetSystems();
        }

        [Route("moduletrend/{dateFrom}/{dateTo}/{systemId?}")] // Can't have two optional parameters in the url, the second one goes to query string.
        [HttpGet]
        public IList<TrendItem> GetModuleTrend(DateTime dateFrom, DateTime dateTo, int? systemId = null, int? branchId = null)
        {
            return _repository.GetModuleTrend(branchId, systemId, dateFrom, dateTo);
        }

        [Route("modules/{dateId:int}/{systemId?}")]
        [HttpGet]
        public IList<ModuleItem> GetModules(int dateId, int? systemId = null, int? branchId = null)
        {
            return _repository.GetModules(branchId, systemId, dateId);
        }

        // 2 - modules
        [Route("moduleslist/{systemId}")]
        [HttpGet]
        public IList<Module> GetModulesBySystem(int systemId)
        {
            return systemId == -1 ? _repository.GetAllModules() :  _repository.GetModulesBySystem(systemId);
        }

        [Route("namespacetrend/{moduleId}/{dateFrom}/{dateTo}/{branchId?}")]
        [HttpGet]
        public IList<TrendItem> GetNamespace(int moduleId, DateTime dateFrom, DateTime dateTo, int? branchId = null)
        {
            return _repository.GetNamespaceTrend(branchId, moduleId, dateFrom, dateTo);
        }

        [Route("namespaces/{moduleId}/{dateId:int}/{branchId?}")]
        [HttpGet]
        public IList<NamespaceItem> Get(int moduleId, int dateId, int? branchId = null)
        {
            return _repository.GetNamespaces(branchId, moduleId, dateId);
        }

        // 3 - namespaces
        [Route("namespaces/{moduleId}")]
        [HttpGet]
        public IList<Namespace> GetNamespacesByModule(int moduleId)
        {
            return _repository.GetNamespacesByModule(moduleId);
        }

        [Route("typetrend/{moduleId}/{namespaceId}/{dateFrom}/{dateTo}/{branchId?}")]
        [HttpGet]
        public IList<TrendItem> GetTypeTrend(int moduleId, int namespaceId, DateTime dateFrom, DateTime dateTo, int? branchId = null)
        {
            return _repository.GetTypeTrend(branchId, moduleId, namespaceId, dateFrom, dateTo);
        }

        [Route("types/{moduleId}/{namespaceId}/{dateId:int}/{branchId?}")]
        [HttpGet]
        public IList<TypeItem> GetTypes(int moduleId, int namespaceId, int dateId, int? branchId = null)
        {
            return _repository.GetTypes(branchId, moduleId, namespaceId, dateId);
        }

        // 4 - types
        [Route("types/{moduleId}/{namespaceId}")]
        [HttpGet]
        public IList<ViewModels.Type> GetTypesByNamespace(int moduleId, int namespaceId)
        {
            return _repository.GetTypesByNamespace(moduleId, namespaceId);
        }

        [Route("membertrend/{moduleId}/{namespaceId}/{typeId}/{dateFrom}/{dateTo}/{branchId?}")]
        [HttpGet]
        public IList<TrendItem> GetMemberTrend(int moduleId, int namespaceId, int typeId, DateTime dateFrom, DateTime dateTo, int? branchId = null)
        {
            return _repository.GetMemberTrend(branchId, moduleId, namespaceId, typeId, dateFrom, dateTo);
        }

        [Route("members/{moduleId}/{namespaceId}/{typeId}/{dateId:int}/{branchId?}")]
        [HttpGet]
        public IList<MemberItem> GetMembers(int moduleId, int namespaceId, int typeId, int dateId, int? branchId = null)
        {
            return _repository.GetMembers(branchId, moduleId, namespaceId, typeId, dateId);
        }

        // 5 - members
        [Route("members/{moduleId}/{namespaceId}/{typeId}")]
        [HttpGet]
        public IList<Member> GetMembersByType(int moduleId, int namespaceId, int typeId)
        {
            return _repository.GetMembersByType(moduleId, namespaceId, typeId);
        }

        [Route("singlemembertrend/{moduleId}/{namespaceId}/{typeId}/{memberId}/{dateFrom}/{dateTo}/{branchId?}")]
        [HttpGet]
        public IList<TrendItem> GetSingleMemberTrend(int moduleId, int namespaceId, int typeId,  int memberId, DateTime dateFrom, DateTime dateTo, int? branchId = null)
        {
            return _repository.GetSingleMemberTrend(branchId, moduleId, namespaceId, typeId, memberId, dateFrom, dateTo);
        }

        [Route("systemstats")]
        [HttpGet]
        public IList<ViewModels.SystemStats> GetSystemStats()
        {
            return _repository.GetLatestSystemStats();
        }

        [Route("dates")]
        [HttpGet]
        public IList<ViewModels.DataPoint> GetDatePoints()
        {
            return _repository.GetDatePoints();
        }

        [Route("systems/{dateId}")]
        [HttpGet]
        public IList<ViewModels.MetricsItem> GetSystemsByDate(int dateId)
        {
            return _repository.GetSystemsByDate(dateId);
        }

        [Route("branches")]
        [HttpGet]
        public IList<ViewModels.Branch> GetBranches()
        {
            return _repository.GetBranches();
        }
    }
}
