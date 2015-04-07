'use strict';

churnModule.controller("ChurnController", function ($scope, churnService, $log) {
    // shared with child controllers       
    $scope.criteria = {};
    $scope.selectedDate = {};
    $scope.repos = [];
    $scope.commits = [];
    $scope.topFiles = [];
    $scope.repo = "";
    // end
    
    $scope.init = function () {
        $scope.refreshChart($scope.criteria);
    }
    
    $scope.refreshChart = function (criteria) {
        $scope.clear();
        churnService.getTrend(criteria.dateFrom, criteria.dateTo) // Can't bind directly to Wijmo
            .$promise.then(function (data) {
                $scope.trendData = data;
                $scope.$broadcast("adjust_chart");                
            });
    }
    
    $scope.commitClick = function (commit) {
        commit.expanded = !commit.expanded;        
        if (commit.files.length == 0 && commit.expanded) {            
            commit.files = churnService.getFilesByCommit(commit.commitId, $scope.criteria.extension);
        }
    }

    $scope.clear = function() {
        $scope.commits = [];
        $scope.selectedDate = {};
        $scope.topFiles = [];
        $scope.repo = "";
    }
});

