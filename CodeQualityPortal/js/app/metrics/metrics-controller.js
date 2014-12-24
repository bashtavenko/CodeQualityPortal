'use strict';

metricsModule.controller("MetricsController", function ($scope, bootstrappedData, $resource, $log) {    
    var dateFrom = new Date();
    dateFrom.setDate(dateFrom.getDate() - 14);

    var minDate = new Date();
    minDate.setDate(minDate.getDate() - 365);
    $scope.minDate = minDate;

    $scope.maxDate = new Date();

    $scope.criteria = {
        tagOptions: bootstrappedData.tagOptions,
        dateFrom: dateFrom,
        dateTo: new Date(),        
    };

    if ($scope.criteria.tagOptions.length > 0) {
        $scope.criteria.tag = $scope.criteria.tagOptions[0];
    }

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
                
    $scope.init = function () {
        $scope.metricsType = $scope.seriesIndex;
        $scope.refresh();
    }    

    $scope.refresh = function () {
        if ($scope.criteriaForm != undefined) {
            // Setting min and max dates don't stop from typing in dates that are out of range
            $scope.criteriaForm.dateFrom.$setValidity('valid', $scope.criteria.dateFrom >= $scope.minDate && $scope.criteria.dateFrom <= $scope.maxDate);
            $scope.criteriaForm.dateTo.$setValidity('valid', $scope.criteria.dateTo >= $scope.minDate && $scope.criteria.dateTo <= $scope.maxDate);
            if ($scope.criteriaForm.$invalid) {
                return;
            }
        }                
        var params = {
            tag: $scope.criteria.tag,
            dateFrom: $scope.criteria.dateFrom.toISOString().slice(0, 10).replace(/-/g, '-'),
            dateTo: $scope.criteria.dateTo.toISOString().slice(0, 10).replace(/-/g, '-')
        };

        // Wijmo x-axis labels
        var timeDiff = Math.abs($scope.criteria.dateFrom.getTime() - $scope.criteria.dateTo.getTime());
        var dayDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));
        $scope.labelsVisible = dayDiff < 25;

        $scope.selectedDate = null;
        $scope.items = [];
        
        var Trend = $resource("/api/moduletrend/:tag/:dateFrom/:dateTo",
            { tag: "@tag", dateFrom: "@dateFrom", dateTo: "@dateTo" });
        
        Trend.query(params,
            function (data) {
                $scope.trendData = data;
            },
            function (error) {
                $log.error(error);
            });        
    }

    $scope.trendClick = function () {
        $scope.selectedDate = $scope.trendSelection.collectionView.currentItem.date;

        var params = {
            tag: $scope.criteria.tag,
            dateId: $scope.trendSelection.collectionView.currentItem.dateId
        };

        var Modules = $resource("/api/modules/:tag/:dateId",
            { tag: "@tag", dateId: "@dateId"});
        Modules.query(params,
            function (data) {
                $scope.items = data
            },
            function (error) {
                $log.error(error);
            });
    }

    $scope.openDateFrom = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.dateFromOpened = true;
    };

    $scope.openDateTo = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.dateToOpened = true;
    };

    $scope.error = function (name) {
        var s = $scope.criteriaForm[name];
        return s.$invalid ? "has-error" : "";
    }
});

