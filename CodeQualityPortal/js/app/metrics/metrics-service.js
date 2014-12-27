'use strict';

metricsModule.factory("metricsService", function($resource, commonService) {
    return {        
        getTagTrend: function (tag, dateFrom, dateTo) {
            var params = {
                tag: tag,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo)
            };

            return $resource("/api/moduletrend/:tag/:dateFrom/:dateTo",
                { tag: "@tag", dateFrom: "@dateFrom", dateTo: "@dateTo" })
                .query(params);
        },
        getModulesByDate: function (tag, dateId) {
            var params = {
                tag: tag,
                dateId: dateId
            };
            return $resource("/api/modules/:tag/:dateId", { tag: "@tag", dateId: "@dateId" })
                .query(params);
        },

        // Module -> Namespace
        getModules: function (tag) {
            var params = {
            tag: tag                
            };
            return $resource("/api/modules/:tag", { tag: "@tag" })
            .query(params);
        },
        getNamespaceTrend: function (moduleId, dateFrom, dateTo) {
            var params = {
                moduleId: moduleId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo)
            };

            return $resource("/api/namespacetrend/:moduleId/:dateFrom/:dateTo",
                { moduleId: "@moduleId", dateFrom: "@dateFrom", dateTo: "@dateTo" })
                .query(params);
        },
        getNamespacesByDate: function (moduleId, dateId) {
            var params = {
                moduleId: moduleId,
                dateId: dateId
            };
            return $resource("/api/namespaces/:moduleId/:dateId", { moduleId: "@moduleId", dateId: "@dateId" })
                .query(params);
        }
    };
});