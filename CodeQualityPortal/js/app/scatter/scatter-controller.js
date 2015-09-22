'use strict';

scatterModule.controller("ScatterController", function ($scope, bootstrappedData, $log, scatterService) {
    $scope.criteria = {};
    $scope.scatterChart = null;

    $scope.dates = bootstrappedData.dates;    
    $scope.cdi = $scope.dates.length - 1;
    
    $scope.getDate = function () {        
        return $scope.dates[$scope.cdi];
    };

    $scope.goForward = function () {
        $scope.cdi = $scope.cdi + 1;
        $scope.refreshChart();
    };

    $scope.goBack = function () {
        $scope.cdi = $scope.cdi - 1;
        $scope.refreshChart();
    };

    $scope.canGoForward = function () {
        return ($scope.cdi < $scope.dates.length - 1);
    };

    $scope.canGoBack = function () {
        return ($scope.cdi > 0);
    };

    $scope.init = function () {
        $scope.refreshChart();
    }

    $scope.refreshChart = function () {        
        var dateId = $scope.getDate().dateId;
        scatterService.getSystems(dateId)
            .$promise.then(
                function (data) {
                    $scope.scatterData = data;                    
                }
        );
    }
        
    $scope.$watch('scatterChart', function () {
        var chart = $scope.scatterChart;
        if (!chart) {
            return;
        }
        $log.write("++");
        chart.tooltip.content = function (ht) {
            $log.write(ht);
            return ht.name;
        }
    });


    
});

