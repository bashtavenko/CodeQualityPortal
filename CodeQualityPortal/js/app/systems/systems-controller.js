'use strict';

systemsModule.controller("SystemsController", function ($scope, bootstrappedData, $log, systemsService) {
    $scope.init = function () {
        $scope.refreshGrid();
    }

    $scope.refreshGrid = function () {
        // Can't bind directly to items since wijmo doesn't like promise
        systemsService.getStats()
            .$promise.then(
                function (data) {
                    $scope.items = data;
                }
        );
    }

    $scope.trendClick = function (systemName, metricsName, dataPoints, max) {
        $scope.label = systemName + " - " + metricsName;
        $scope.dataPoints = dataPoints;
        $scope.max = max;
        $scope.labelsVisible = dataPoints.length <= 10;
    }
});

