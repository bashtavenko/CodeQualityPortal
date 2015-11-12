'use strict';

metricsModule.factory("metricsService", function($resource, commonService) {
    return {        
        // 1 - system
        getModuleTrend: function (branch, system, dateFrom, dateTo) {
            var params = {
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo),
                systemId: system.id > 0 ? system.id : null,
                branchId: branch.id > 0 ? branch.id : null
            };

            return $resource("/api/moduletrend/:dateFrom/:dateTo/:systemId",
                { dateFrom: "@dateFrom", dateTo: "@dateTo", systemId: "@systemId", branchId: "@branchId" })
                .query(params);
        },
        getModulesByDate: function (branchId, systemId, dateId) {
            var params = {
                dateId: dateId,
                systemId: systemId > 0 ? systemId : null,
                branchId: branchId > 0 ? branchId : null
            };
            return $resource("/api/modules/:dateId/:systemId", { dateId: "@dateId", systemId: "@systemId", branchId: "@branchId" })
                .query(params);
        },

        // 2 - module
        getModules: function (systemId) {
            var params = { systemId: systemId };
            return $resource("/api/moduleslist/:systemId", { systemId: "@systemId" }).query(params);
        },
        getNamespaceTrend: function (branchId, moduleId, dateFrom, dateTo) {
            var params = {
                moduleId: moduleId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo),
                branchId: branchId > 0 ? branchId : null
            };
            return $resource("/api/namespacetrend/:moduleId/:dateFrom/:dateTo/:branchId",
                { moduleId: "@moduleId", dateFrom: "@dateFrom", dateTo: "@dateTo", branchId: "@branchId" })
                .query(params);
        },
        getNamespacesByDate: function (branchId, moduleId, dateId) {
            var params = {
                moduleId: moduleId,
                dateId: dateId,
                branchId: branchId > 0 ? branchId : null
            };
            return $resource("/api/namespaces/:moduleId/:dateId/:branchId", { moduleId: "@moduleId", dateId: "@dateId", branchId: "@branchId" })
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
        getTypeTrend: function (branchId, moduleId, namespaceId, dateFrom, dateTo) {
            var params = {
                moduleId: moduleId,                
                namespaceId: namespaceId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo),
                branchId: branchId > 0 ? branchId : null
            };

            return $resource("/api/typetrend/:moduleId/:namespaceId/:dateFrom/:dateTo/:branchId",
                { moduleId: "@moduleId", namespaceId: "@namespaceId", dateFrom: "@dateFrom", dateTo: "@dateTo", branchId: "@branchId" })
                .query(params);
        },
        getTypesByDate: function (branchId, moduleId, namespaceId, dateId) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId,
                dateId: dateId,
                branchId: branchId > 0 ? branchId : null
            };
            return $resource("/api/types/:moduleId/:namespaceId/:dateId/:branchId", { moduleId: "@moduleId", namespaceId: "@namespaceId", dateId: "@dateId", branchId: "@branchId" })
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

        getMemberTrend: function (branchId, moduleId, namespaceId, typeId, dateFrom, dateTo) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId,                
                typeId: typeId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo),
                branchId: branchId > 0 ? branchId : null
            };

            return $resource("/api/membertrend/:moduleId/:namespaceId/:typeId/:dateFrom/:dateTo/:branchId",
                { moduleId: "@moduleId", namespaceId: "@namespaceId", typeId: "@typeId", dateFrom: "@dateFrom", dateTo: "@dateTo", branchId: "@branchId" })
                    .query(params);
        },

        getMembersByDate: function (branchId, moduleId, namespaceId, typeId, dateId) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId,
                typeId: typeId,
                dateId: dateId,
                branchId: branchId > 0 ? branchId : null
            };
            return $resource("/api/members/:moduleId/:namespaceId/:typeId/:dateId/:branchId", { moduleId: "@moduleId", namespaceId: "@namespaceId", typeId: "@typeId", dateId: "@dateId", branchId: "@branchId" })
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

        getSingleMemberTrend: function (branchId, moduleId, namespaceId, typeId, memberId, dateFrom, dateTo) {
            var params = {
                moduleId: moduleId,
                namespaceId: namespaceId,
                typeId: typeId,                
                memberId: memberId,
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo),
                branchId: branchId > 0 ? branchId : null
            };

            return $resource("/api/singlemembertrend/:moduleId/:namespaceId/:typeId/:memberId/:dateFrom/:dateTo/:branchId",
                { moduleId: "@moduleId", namespaceId: "@namespaceId", typeId: "@typeId", memberId: "@memberId", dateFrom: "@dateFrom", dateTo: "@dateTo", branchId: "@branchId" })
                    .query(params);
        }
    };
});