'use strict';

churnModule.factory("maintenanceService", function ($resource) {
    return {        
        getList: function (category) {
            var params = {
                category: category
            };
            return $resource("/api/maintenance/lists/:category", { category: "@category" })
                .query(params).$promise;
        },
        getModules: function (category, value) {
            var params = {
                category: category,
                value: value
            };
            return $resource("/api/maintenance/modules/:category/:value", { category: "@category", value: "@value" })
                .query(params).$promise;
        }
    };
});