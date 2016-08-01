'use strict';

churnModule.factory("coverageService", function ($resource) {
    return {        
        getCoverage: function (summaryBy) {
            var params = {
                summaryBy: summaryBy
            };
            return $resource("/api/codecoverage-summary/:summaryBy", { summaryBy: "@summaryBy" })
                .get(params).$promise;
        },
        getModuleStats: function(category, categoryId, dateId) {
            var params = {
                category: category,
                categoryId: categoryId,
                dateId: dateId
            };
            return $resource("/api/module-stats/:category/:categoryId/:dateId", { category: "@category", categoryId: "@categoryId", dateId: "@dateId" })
                .get(params).$promise;
        }
    };
});