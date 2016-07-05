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
        private readonly IMetricsRepository _metricsRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly ISummaryRepository _summaryRepository;

        public MetricsController(IMetricsRepository metricsRepository, IBranchRepository branchRepository, ISummaryRepository summaryRepository)
        {
            _metricsRepository = metricsRepository;
            _branchRepository = branchRepository;
            _summaryRepository = summaryRepository;
        }

        // 1 - systems
        [Route("systems")]
        public IList<ViewModels.SystemDefinition> Get()
        {
            return _metricsRepository.GetSystems();
        }

        [Route("moduletrend/{dateFrom}/{dateTo}/{systemId?}")] // Can't have two optional parameters in the url, the second one goes to query string.
        [HttpGet]
        public IList<TrendItem> GetModuleTrend(DateTime dateFrom, DateTime dateTo, int? systemId = null, int? branchId = null)
        {
            return _metricsRepository.GetModuleTrend(branchId, systemId, dateFrom, dateTo);
        }

        [Route("modules/{dateId:int}/{systemId?}")]
        [HttpGet]
        public IList<ModuleItem> GetModules(int dateId, int? systemId = null, int? branchId = null)
        {
            return _metricsRepository.GetModules(branchId, systemId, dateId);
        }

        // 2 - modules
        [Route("moduleslist/{systemId}")]
        [HttpGet]
        public IList<Module> GetModulesBySystem(int systemId)
        {
            return systemId == -1 ? _metricsRepository.GetAllModules() :  _metricsRepository.GetModulesBySystem(systemId);
        }

        [Route("namespacetrend/{moduleId}/{dateFrom}/{dateTo}/{branchId?}")]
        [HttpGet]
        public IList<TrendItem> GetNamespace(int moduleId, DateTime dateFrom, DateTime dateTo, int? branchId = null)
        {
            return _metricsRepository.GetNamespaceTrend(branchId, moduleId, dateFrom, dateTo);
        }

        [Route("namespaces/{moduleId}/{dateId:int}/{branchId?}")]
        [HttpGet]
        public IList<NamespaceItem> Get(int moduleId, int dateId, int? branchId = null)
        {
            return _metricsRepository.GetNamespaces(branchId, moduleId, dateId);
        }

        // 3 - namespaces
        [Route("namespaces/{moduleId}")]
        [HttpGet]
        public IList<Namespace> GetNamespacesByModule(int moduleId)
        {
            return _metricsRepository.GetNamespacesByModule(moduleId);
        }

        [Route("typetrend/{moduleId}/{namespaceId}/{dateFrom}/{dateTo}/{branchId?}")]
        [HttpGet]
        public IList<TrendItem> GetTypeTrend(int moduleId, int namespaceId, DateTime dateFrom, DateTime dateTo, int? branchId = null)
        {
            return _metricsRepository.GetTypeTrend(branchId, moduleId, namespaceId, dateFrom, dateTo);
        }

        [Route("types/{moduleId}/{namespaceId}/{dateId:int}/{branchId?}")]
        [HttpGet]
        public IList<TypeItem> GetTypes(int moduleId, int namespaceId, int dateId, int? branchId = null)
        {
            return _metricsRepository.GetTypes(branchId, moduleId, namespaceId, dateId);
        }

        // 4 - types
        [Route("types/{moduleId}/{namespaceId}")]
        [HttpGet]
        public IList<ViewModels.Type> GetTypesByNamespace(int moduleId, int namespaceId)
        {
            return _metricsRepository.GetTypesByNamespace(moduleId, namespaceId);
        }

        [Route("membertrend/{moduleId}/{namespaceId}/{typeId}/{dateFrom}/{dateTo}/{branchId?}")]
        [HttpGet]
        public IList<TrendItem> GetMemberTrend(int moduleId, int namespaceId, int typeId, DateTime dateFrom, DateTime dateTo, int? branchId = null)
        {
            return _metricsRepository.GetMemberTrend(branchId, moduleId, namespaceId, typeId, dateFrom, dateTo);
        }

        [Route("members/{moduleId}/{namespaceId}/{typeId}/{dateId:int}/{branchId?}")]
        [HttpGet]
        public IList<MemberItem> GetMembers(int moduleId, int namespaceId, int typeId, int dateId, int? branchId = null)
        {
            return _metricsRepository.GetMembers(branchId, moduleId, namespaceId, typeId, dateId);
        }

        // 5 - members
        [Route("members/{moduleId}/{namespaceId}/{typeId}")]
        [HttpGet]
        public IList<Member> GetMembersByType(int moduleId, int namespaceId, int typeId)
        {
            return _metricsRepository.GetMembersByType(moduleId, namespaceId, typeId);
        }

        [Route("singlemembertrend/{moduleId}/{namespaceId}/{typeId}/{memberId}/{dateFrom}/{dateTo}/{branchId?}")]
        [HttpGet]
        public IList<TrendItem> GetSingleMemberTrend(int moduleId, int namespaceId, int typeId,  int memberId, DateTime dateFrom, DateTime dateTo, int? branchId = null)
        {
            return _metricsRepository.GetSingleMemberTrend(branchId, moduleId, namespaceId, typeId, memberId, dateFrom, dateTo);
        }

        [Route("systemstats")]
        [HttpGet]
        public IList<ViewModels.SystemStats> GetSystemStats()
        {
            return _summaryRepository.GetLatestSystemStats();
        }

        [Route("dates")]
        [HttpGet]
        public IList<ViewModels.DataPoint> GetDatePoints()
        {
            return _summaryRepository.GetDatePoints();
        }

        [Route("systems/{dateId}")]
        [HttpGet]
        public IList<ViewModels.MetricsItem> GetSystemsByDate(int dateId)
        {
            return _summaryRepository.GetSystemsByDate(dateId);
        }

        [Route("branches")]
        [HttpGet]
        public IList<ViewModels.Branch> GetBranches()
        {
            return _branchRepository.GetBranches();
        }

        [Route("branchdates/{branchId?}")]
        [HttpGet]
        public IList<ViewModels.DataPoint> GetBranchDates(int? branchId = null)
        {
            return _branchRepository.GetBranchCollectionDates(branchId);
        }

        [Route("branchdiff/{dateAId}/{dateBId}")]
        [HttpGet]
        public BranchDiff GetBranchDiff(int dateAId, int dateBId, int? branchAId = null, int? branchBId = null)
        {
            return _branchRepository.GetBranchDiff(branchAId, dateAId, branchBId, dateBId);
        }

        [Route("codecoverage-summary/{summaryBy}")]
        public CodeCoverageSummary GetCodeCoverageSummary(Category summaryBy)
        {
            return _summaryRepository.GetCoverageSummary(90, summaryBy);
        }
    }
}
