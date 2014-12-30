'use strict';

// Populates "criteria.SelectedDate" in parent scope
metricsModule.controller("ChartController", function ($scope, bootstrappedData, $resource, $log) {

    $scope.chartProps = {
        selection: null
    };

    $scope.seriesIndex = {
        key: "maintainabilityIndex",
        value: "Maintainability Index",
        min: 0,
        max: 100
    };
    $scope.seriesLoc = {
        key: "linesOfCode",
        value: "Lines of Code"
    };
    $scope.seriesComplexity = {
        key: "cyclomaticComplexity",
        value: "CyclomaticComplexity"
    };
    $scope.seriesCoupling = {
        key: "classCoupling",
        value: "Class Coupling"
    };
    $scope.seriesDepth = {
        key: "depthOfInheritance",
        value: "Depth of Inheritance",
        min: 0
    };

    $scope.metricsType = $scope.seriesIndex;

    $scope.trendClick = function () {
        if ($scope.chartProps.selection != null) {
            $scope.criteria.selectedDate = $scope.chartProps.selection.collectionView.currentItem;
            $scope.refreshGrid();
        }
    }

    $scope.$on("adjust_chart", function (event, message) {
        // Wijmo x-axis labels
        var timeDiff = Math.abs($scope.criteria.dateFrom.getTime() - $scope.criteria.dateTo.getTime());
        var dayDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));
        $scope.labelsVisible = dayDiff < 25;
    });    
});

