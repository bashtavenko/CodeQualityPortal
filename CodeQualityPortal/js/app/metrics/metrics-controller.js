'use strict';

metricsModule.controller("MetricsController", function ($scope, bootstrappedData, $resource, $log, metricsService) {    
    $scope.criteria = {}; // shared with child controllers   
                    
    $scope.init = function () {        
        $scope.refreshChart();
    }    

    $scope.refreshChart = function () {
        metricsService.getTagTrend($scope.criteria.tag, $scope.criteria.dateFrom, $scope.criteria.dateTo)
            .$promise.then(
                function(data) {
                    $scope.criteria.selectedDate = null;
                    $scope.items = [];
                    $scope.$broadcast("adjust_chart");
                    $scope.trendData = data;
                },
                function (error) {
                    $log.error(error);
                });
    }

    $scope.refreshGrid = function () {
        // Can't bind directly to items since wijmo doesn't like promise
        metricsService.getModulesByDate($scope.criteria.tag, $scope.criteria.selectedDate.dateId)
            .$promise.then(
                function (data) {
                    $scope.items = data;
                }
        );
    }
});

