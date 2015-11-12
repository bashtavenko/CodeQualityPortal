'use strict';

branchDiffModule.factory("branchDiffService", function($resource) {
    return {        
        getDates: function (branchId) {
            var params = {
                branchId: branchId > 0 ? branchId : null                
            };
            return $resource("/api/branchDates/:branchId", { branchId: "@branchId" })
                .query(params);
        },
        getData: function (branchAId, dateAId, branchBId, dateBId) {
            var params = {
                dateAId: dateAId,
                dateBId: dateBId,
                branchAId: branchAId > 0 ? branchAId : null,
                branchBId: branchBId > 0 ? branchBId : null
            };
            // This is "get" and not "query" because the dataset isn't an array
            return $resource("/api/branchdiff/:dateAId/:dateBId", { dateAId: "@dateAId", dateBId: "@dateBId", branchAId: "@branchAId", branchBId: "@branchBId" })
                .get(params); 
        }
    };
});