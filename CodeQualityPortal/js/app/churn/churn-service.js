'use strict';

churnModule.factory("churnService", function($resource, commonService) {
    return {
        getTrend: function(dateFrom, dateTo){
            var params = {
                dateFrom: commonService.convertDateToIso(dateFrom),
                dateTo: commonService.convertDateToIso(dateTo)
            };
            return $resource("/api/trend/:dateFrom/:dateTo",
                { dateFrom: "@dateFrom", dateTo: "@dateTo" })
                .query(params);        
        },
        getSummary: function(dateId){
            var params = {
                dateId: dateId
            };
            return $resource("/api/reposummary/:dateId",
                { dateId: "@dateId" })
                .query(params);        
        },
        getFiles: function (repoId, dateId, topX) {
            var params = {
                repoId: repoId,
                dateId: dateId,
                topX: topX
            };
            return $resource("/api/files/:repoId/:dateId",
                { repoId: "@repoId", dateId: "@dateId", topX: "@topX" })
                .query(params);
        },
        getCommits: function (repoId, dateId) {
            var params = {
                repoId: repoId,
                dateId: dateId
            };
            return $resource("/api/commits/:repoId/:dateId",
                { repoId: "@repoId", dateId: "@dateId" })
                .query(params);
        },
        getFilesByCommit: function (commitId, extension) {
            var params = {
                commitId: commitId
            };

            return $resource("/api/files/:commitId",
                { commitId: "@commitId" })
                .query(params);
        },
    }
});