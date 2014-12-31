'use strict';

churnModule.controller("ChurnController", function ($scope, churnService, $log) {
    // shared with child controllers       
    $scope.criteria = {};
    $scope.selectedDate = {};
    $scope.commits = [];
    $scope.topFiles = [];    
    // end
    
    $scope.init = function () {
        $scope.refreshChart($scope.criteria);
    }

    $scope.refreshChart = function (criteria) {        
        $scope.commits = [];
        $scope.selectedDate = null;
        $scope.topFiles = [];
        churnService.getTrend(criteria.repo.repoId, criteria.dateFrom, criteria.dateTo, criteria.extension) // Can't bind directly to Wijmo
            .$promise.then(function (data) {
                $scope.$broadcast("adjust_chart");
                $scope.trendData = data;
            });
    }
    
    $scope.commitClick = function (commit) {
        commit.expanded = !commit.expanded;        
        if (commit.files.length == 0 && commit.expanded) {            
            commit.files = churnService.getFilesByCommit(commit.commitId, $scope.criteria.extension);
        }
    }
});

