'use strict';

churnModule.factory("coverageService", function ($resource) {
    return {        
        getCoverage: function (summaryBy) {
            var params = {
                summaryBy: summaryBy
            };
            return $resource("/api/codecoverage-summary/:summaryBy", { summaryBy: "@summaryBy" })
                .get(params).$promise;
        }
    };
});