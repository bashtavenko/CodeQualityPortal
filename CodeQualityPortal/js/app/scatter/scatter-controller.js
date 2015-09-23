'use strict';

scatterModule.controller("ScatterController", function ($scope, bootstrappedData, $log, scatterService) {    
    $scope.chartProps = {
        selection: null
    };    

    $scope.dates = bootstrappedData.dates;    
    $scope.cdi = $scope.dates.length - 1;
    $scope.minCodeCoverage = 100;
    $scope.maxLinesOfCode = 0;
    
    $scope.getDate = function () {        
        return $scope.dates[$scope.cdi];
    };

    $scope.getMaxLinesOfCode = function () {
        return $scope.maxLinesOfCode + $scope.maxLinesOfCode * 0.03;
    }

    $scope.getMinCodeCoverage = function () {
        return $scope.minCodeCoverage == 0 ? -7 : $scope.minCodeCoverage;
    }

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
        $scope.selectedSystem = null;
        $scope.modulesData = null;
        scatterService.getSystems(dateId)
            .$promise.then(
                function (data) {
                    // Wijmo can't bind series to 'name' property
                    // We also want to adjust chart to render outliers better                    
                    for (var i = 0; i < data.length; i++) {
                        data[i].systemName = data[i].name;
                        if ($scope.minCodeCoverage > data[i].codeCoverage) {
                            $scope.minCodeCoverage = data[i].codeCoverage;
                        }
                        if ($scope.maxLinesOfCode < data[i].linesOfCode) {
                            $scope.maxLinesOfCode = data[i].linesOfCode;
                        }
                    }
                    $scope.scatterData = data;                    
                }
        );
    }

    $scope.systemClick = function () {
        if ($scope.chartProps.selection != null) {
            var item = $scope.chartProps.selection.collectionView.currentItem;
            $scope.selectedSystem = item.name;
            var systemId = item.id;
            var dateId = $scope.getDate().dateId;
            scatterService.getModulesByDate(systemId, dateId)
                .$promise.then(
                    function (data) {
                        $scope.modulesData = data;
                    }
            );
        }
    }    
});

