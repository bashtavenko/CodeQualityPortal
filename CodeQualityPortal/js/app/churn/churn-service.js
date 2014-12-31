'use strict';

churnModule.factory("churnService", function($resource) {
    return {
        getTrend: function(repoId, dateFrom, dateTo, extension){
            var params = {
                repoId: repoId,
                dateFrom: dateFrom.toISOString().slice(0, 10).replace(/-/g, '-'),
                dateTo: dateTo.toISOString().slice(0, 10).replace(/-/g, '-'),
                fileExtension: extension
            };
            return $resource("/api/trend/:repoId/:dateFrom/:dateTo/:fileExtension",
                { repoId: "@repoId", dateFrom: "@dateFrom", dateTo: "@dateTo", fileExtension: "@fileExtension" })
                .query(params);        
        },
        getFiles: function (repoId, dateId, extension, topX) {
            var params = {
                repoId: repoId,
                dateId: dateId,
                fileExtension: extension,
                topX: topX
            };
            return $resource("/api/files/:repoId/:dateId/:fileExtension",
                { repoId: "@repoId", dateId: "@dateId", fileExtension: "@fileExtension", topX: "@topX" })
                .query(params);
        },
        getCommits: function (repoId, dateId, extension) {
            var params = {
                repoId: repoId,
                dateId: dateId,
                fileExtension: extension
            };
            return $resource("/api/commits/:repoId/:dateId/:fileExtension",
                { repoId: "@repoId", dateId: "@dateId", fileExtension: "@fileExtension" })
                .query(params);
        },
        getFilesByCommit: function (commitId, extension) {
            var params = {
                commitId: commitId,
                fileExtension: extension
            };

            return $resource("/api/files/:commitId/:fileExtension",
                { commitId: "@commitId", fileExtension: "@fileExtension" })
                .query(params);
        },
    }
});