'use strict';

/// <reference path="churn-controller.js" />

churnModule.controller("ChurnController", function ($scope, bootstrappedData, $resource, $log, $http) {    
    var dateFrom = new Date();
    dateFrom.setDate(dateFrom.getDate() - 14);
    $scope.criteria = {
        repoOptions: bootstrappedData.repoOptions,
        dateFrom: dateFrom,
        dateTo: new Date(),
        extension: "cs"
    };

    $scope.criteria.dateFrom = new Date("2014-11-04");
    $scope.criteria.dateTo = new Date("2014-11-14");

    if ($scope.criteria.repoOptions.length > 0) {
        $scope.criteria.repo = $scope.criteria.repoOptions[0];
    }
        
    $scope.init = function () {
        $scope.refresh($scope.criteria);
    }    

    $scope.refresh = function (criteria) {
        var params = {
            repoId: criteria.repo.repoId,
            dateFrom: criteria.dateFrom.toISOString().slice(0, 10).replace(/-/g, '-'),
            dateTo: criteria.dateTo.toISOString().slice(0, 10).replace(/-/g, '-'),
            fileExtension: criteria.extension
        };        
        
        var Trend = $resource("/api/trend/:repoId/:dateFrom/:dateTo/:fileExtension",
            { repoId: "@repoId", dateFrom: "@dateFrom", dateTo: "@dateTo", fileExtension: "@fileExtension" });
            
        $scope.commitsData = [];
        $scope.selectedDate = null;
        Trend.query(params,
            function (data) {
                $scope.trendData = data;
            },
            function (error) {
                $log.error(error);
            });        
    }

    $scope.trendClick = function () {
        $scope.selectedDate = $scope.trendSelection.collectionView.currentItem.date;

        var params = {
            repoId: $scope.criteria.repo.repoId,
            dateId: $scope.trendSelection.collectionView.currentItem.dateId,
            fileExtension: $scope.criteria.extension
        };

        var Commits = $resource("/api/commits/:repoId/:dateId/:fileExtension",
            { repoId: "@repoId", dateId: "@dateId", fileExtension: "@fileExtension" });

        Commits.query(params,
            function (data) {
                $scope.commits = data;
            },
            function (error) {
                $log.error(error);
            });        
    }

    $scope.commitClick = function (commit) {
        if (commit.files != undefined && commit.files.length > 0) {
            commit.files = [];
            return;
        }
        var params = {
            commitId: commit.commitId,            
            fileExtension: $scope.criteria.extension
        };

        var Files = $resource("/api/files/:commitId/:fileExtension",
            { commitId: "@commitId", fileExtension: "@fileExtension" });

        Files.query(params,
            function (data) {
                commit.files = data;
            },
            function (error) {
                $log.error(error);
            });
    }
});