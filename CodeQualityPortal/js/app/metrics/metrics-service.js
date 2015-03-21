'use strict';

metricsModule.factory("metricsService", function($resource, commonService) {
    return {        
        // 1 - system
        getModuleTrend: function (systemId, dateFrom, dateTo) {
            var params = {
                systemId: systemId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo)
            };

            return $resource("/api/moduletrend/:dateFrom/:dateTo/:systemId",
                { dateFrom: "@dateFrom", dateTo: "@dateTo", systemId: "@systemId" })
                .query(params);
        },
        getModulesByDate: function (systemId, dateId) {
            var params = {
                systemId: systemId,
                dateId: dateId
            };
            return $resource("/api/modules/:dateId/:systemId", { dateId: "@dateId", systemId: "@systemId", })
                .query(params);
        },

        // 2 - module
        getModules: function (systemId) {
            var params = {
            systemId: systemId, 
            };
            return $resource("/api/modules/:systemId", { systemId: "@systemId" })
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
            return $resource("/api/namespaces/:moduleId/:dateId", {moduleId: "@moduleId", dateId: "@dateId" })
                .query(params);
        },

        // 3 - namespace
        getNamespaces: function (moduleId) {
            var params = {
                moduleId: moduleId                
            };
            return $resource("/api/namespaces/:moduleId", {moduleId: "@moduleId" })
            .query(params);
        },
        getTypeTrend: function (moduleId, namespaceId, dateFrom, dateTo) {
            var params = {
                moduleId: moduleId,                
                namespaceId: namespaceId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo)
            };

            return $resource("/api/typetrend/:moduleId/:namespaceId/:dateFrom/:dateTo",
                { moduleId: "@moduleId", namespaceId: "@namespaceId", dateFrom: "@dateFrom", dateTo: "@dateTo" })
                .query(params);
        },
        getTypesByDate: function (moduleId, namespaceId, dateId) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId,
                dateId: dateId
            };
            return $resource("/api/types/:moduleId/:namespaceId/:dateId", {moduleId: "@moduleId", namespaceId: "@namespaceId", dateId: "@dateId" })
                .query(params);
        },

        // 4 - type
        getTypes: function (moduleId, namespaceId) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId                
            };
            return $resource("/api/types/:moduleId/:namespaceId", {moduleId: "@moduleId", namespaceId: "@namespaceId" })
                .query(params);
        },

        getMemberTrend: function (moduleId, namespaceId, typeId, dateFrom, dateTo) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId,                
                typeId: typeId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo)
            };

            return $resource("/api/membertrend/:moduleId/:namespaceId/:typeId/:dateFrom/:dateTo",
                { moduleId: "@moduleId", namespaceId: "@namespaceId", typeId: "@typeId", dateFrom: "@dateFrom", dateTo: "@dateTo" })
                    .query(params);
        },

        getMembersByDate: function (moduleId, namespaceId, typeId, dateId) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId,
                typeId: typeId,
                dateId: dateId
            };
            return $resource("/api/members/:moduleId/:namespaceId/:typeId/:dateId", { moduleId: "@moduleId", namespaceId: "@namespaceId", typeId: "@typeId", dateId: "@dateId" })
                .query(params);
        },

        // 5 - member
        getMembers: function (moduleId, namespaceId, typeId) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId,
                typeId: typeId                
            };
            return $resource("/api/members/:moduleId/:namespaceId/:typeId", { moduleId: "@moduleId", namespaceId: "@namespaceId", typeId: "@typeId" })
                .query(params);
        },

        getSingleMemberTrend: function (moduleId, namespaceId, typeId, memberId, dateFrom, dateTo) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId,
                typeId: typeId,                
                memberId: memberId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo)
            };

            return $resource("/api/singlemembertrend/:moduleId/:namespaceId/:typeId/:memberId/:dateFrom/:dateTo",
                { moduleId: "@moduleId", namespaceId: "@namespaceId", typeId: "@typeId", memberId: "@memberId", dateFrom: "@dateFrom", dateTo: "@dateTo" })
                    .query(params);
        },
    };
});