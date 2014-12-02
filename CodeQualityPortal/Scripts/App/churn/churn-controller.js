'use strict';

/// <reference path="churn-controller.js" />

churnModule.controller("ChurnController", function ($scope, bootstrappedData, $resource, $log, $http) {    
    var dateFrom = new Date();
    dateFrom.setDate(dateFrom.getDate() - 14);
    $scope.criteria = {
        repoOptions: bootstrappedData.repoOptions,
        dateFrom: dateFrom,
        dateTo: new Date(),
        extension:"cs"
    };
    if ($scope.criteria.repoOptions.length > 0) {
        $scope.criteria.repo = $scope.criteria.repoOptions[0];
    }

    $scope.refresh = function (criteria) {
        var params = {
            repoId: criteria.repo.repoId,
            dateFrom: criteria.dateFrom.toISOString().slice(0, 10).replace(/-/g, '-'),
            dateTo: criteria.dateTo.toISOString().slice(0, 10).replace(/-/g, '-'),
            fileExtension: criteria.extension
        };
        
        $http({method: "GET", url: "/api/trend/" + params.repoId + "/" + params.dateFrom + "/" + params.dateTo + "/" + params.fileExtension}).
            success(function (data, status, headers, config) {
                $scope.data = data;
                $log.info(data);
            }).
            error(function (data, status, headers, config) {
                $log.error(data, status, headers, config);
        });

        // Resource would be better..
        //var Trend = $resource("/api/trend/:repoId/:dateFrom/:dateTo/:fileExtension",
        //    { repoId: "@repoId", dateFrom: "@dateFrom", dateTo: "@dateTo", fileExtension: "@fileExtension" });
            
        //Trend.get(params, function(data) {
        //    $log.info(data);
        //});        
    }
});