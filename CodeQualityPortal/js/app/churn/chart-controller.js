'use strict';

churnModule.controller("ChartController", function ($scope, churnService, $log) {
    $scope.chartProps = {
        selection: null
    };        

    $scope.trendClick = function () {
        if ($scope.chartProps.selection == null) {
            return;
        }

        $scope.$parent.clear();
        $scope.$parent.selectedDate = $scope.chartProps.selection.collectionView.currentItem;

        churnService.getSummary($scope.selectedDate.dateId)
            .$promise.then(function(data) {
                $scope.$parent.repos = data;
            });

    }

    $scope.$on("adjust_chart", function (event, message) {
        // Wijmo x-axis labels
        if ($scope.$parent.trendData.length == 0) {
            $scope.labelsVisible = false;
        }
        else {
            var timeDiff = Math.abs($scope.criteria.dateFrom.getTime() - $scope.criteria.dateTo.getTime());
            var dayDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));
            $scope.labelsVisible = dayDiff < 25;
        }
    });    
});

