'use strict';

churnModule.controller("ChartController", function ($scope, churnService, $log) {
    $scope.chartProps = {
        selection: null
    };        

    $scope.trendClick = function () {
        if ($scope.chartProps.selection == null) {
            return;
        }

        $scope.$parent.selectedDate = $scope.chartProps.selection.collectionView.currentItem;
        $scope.$parent.topFiles = churnService.getFiles($scope.criteria.repo.repoId, $scope.selectedDate.dateId, $scope.criteria.extension, 5);

        churnService.getCommits($scope.criteria.repo.repoId, $scope.selectedDate.dateId, $scope.criteria.extension)
            .$promise.then(function (data) {
                for (var i = 0; i < data.length; i++) {
                    data[i].expanded = false;
                    data[i].files = [];
                }                
                $scope.$parent.commits = data;                
            });        
    }

    $scope.$on("adjust_chart", function (event, message) {
        // Wijmo x-axis labels
        var timeDiff = Math.abs($scope.criteria.dateFrom.getTime() - $scope.criteria.dateTo.getTime());
        var dayDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));
        $scope.labelsVisible = dayDiff < 25;
    });    
});

