'use strict';

// Populates "criteria.SelectedDate" in parent scope
metricsModule.controller("ChartController", function ($scope, bootstrappedData, $resource, $log) {

    $scope.chartProps = {
        selection: null
    };


    $scope.indexAndCoverage = {
        min: 0,
        max: 100,
        series: [{ key: "maintainabilityIndex", value: "Maintainability Index"},
                 { key: "codeCoverage", value: "Code Coverage" }]
    };

    $scope.locAndComplexity = {
        min: 0,
        series: [{ key: "linesOfCode", value: "Lines of Code"},
                 { key: "cyclomaticComplexity", value: "CyclomaticComplexity"}]
    };
    
    $scope.coupling = {
        min: 0,
        series: [{ key: "classCoupling", value: "Class Coupling" }]
    };

    $scope.depth = {
        min: 0,
        series:[{ key: "depthOfInheritance", value: "Depth of Inheritance"}]
    };

    $scope.metricsType = $scope.indexAndCoverage;

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

