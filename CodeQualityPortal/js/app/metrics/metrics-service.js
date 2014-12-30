'use strict';

metricsModule.factory("metricsService", function($resource, commonService) {
    return {        
        // 1 - tag
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

        // 2 - module
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
        },

        // 3 - namespace
        getNamespaces: function (moduleId) {
            var params = {
                moduleId: moduleId                
            };
            return $resource("/api/namespaces/:moduleId", { moduleId: "@moduleId" })
            .query(params);
        },
        getTypeTrend: function (namespaceId, dateFrom, dateTo) {
            var params = {
                namespaceId: namespaceId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo)
            };

            return $resource("/api/typetrend/:namespaceId/:dateFrom/:dateTo",
                { namespaceId: "@namespaceId", dateFrom: "@dateFrom", dateTo: "@dateTo" })
                .query(params);
        },
        getTypesByDate: function (namespaceId, dateId) {
            var params = {
                namespaceId: namespaceId,
                dateId: dateId
            };
            return $resource("/api/types/:namespaceId/:dateId", { namespaceId: "@namespaceId", dateId: "@dateId" })
                .query(params);
        },

        // 4 - type
        getTypes: function (namespaceId) {
            var params = {
                namespaceId: namespaceId                
            };
            return $resource("/api/types/:namespaceId", { moduleId: "@namespaceId" })
            .query(params);
        },

        getMemberTrend: function (typeId, dateFrom, dateTo) {
            var params = {
                typeId: typeId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo)
            };

            return $resource("/api/membertrend/:typeId/:dateFrom/:dateTo",
                { typeId: "@typeId", dateFrom: "@dateFrom", dateTo: "@dateTo" })
                .query(params);
        },

        getMembersByDate: function (typeId, dateId) {
            var params = {
                typeId: typeId,
                dateId: dateId
            };
            return $resource("/api/members/:typeId/:dateId", { typeId: "@typeId", dateId: "@dateId" })
                .query(params);
        },
    };
});