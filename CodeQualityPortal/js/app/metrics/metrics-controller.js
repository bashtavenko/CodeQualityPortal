'use strict';

metricsModule.controller("MetricsController", function ($scope, bootstrappedData, $log, metricsService) {    
    $scope.criteria = {}; // shared with child controllers       
                        
    $scope.init = function () {
        $scope.mode = new $scope.tagMode;
        $scope.refreshChart();
    }

    $scope.refreshChart = function () {
        $scope.mode.getTrend()
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
        $scope.mode.getItems()        
            .$promise.then(
                function (data) {
                    $scope.items = data;
                }
        );
    }

    $scope.tagMode = function () {
        return {
            getTrend: function () {
                return metricsService.getTagTrend($scope.criteria.tag, $scope.criteria.dateFrom, $scope.criteria.dateTo);
            },
            getItems: function () {
                return metricsService.getModulesByDate($scope.criteria.tag, $scope.criteria.selectedDate.dateId);
            },
            trendLabel: "Target trend for " + $scope.criteria.tag,
            itemsLabel: "Modules on"
        };
    };
});

