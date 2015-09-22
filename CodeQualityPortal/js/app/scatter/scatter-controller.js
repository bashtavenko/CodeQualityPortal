'use strict';

scatterModule.controller("ScatterController", function ($scope, bootstrappedData, $log, scatterService) {    
    $scope.chartProps = {
        selection: null
    };    

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
        $scope.selectedSystem = null;
        $scope.modulesData = null;
        scatterService.getSystems(dateId)
            .$promise.then(
                function (data) {
                    // Wijmo can't bind series to 'name' property
                    for (var i = 0; i < data.length; i++) {
                        data[i].systemName = data[i].name;
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

