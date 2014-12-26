'use strict';

// Populates "criteria" in parent scope
metricsModule.controller("CriteriaController", function ($scope, bootstrappedData) {
    var dateFrom = new Date();
    dateFrom.setDate(dateFrom.getDate() - 14);

    var minDate = new Date();
    minDate.setDate(minDate.getDate() - 365);
    $scope.minDate = minDate;

    $scope.maxDate = new Date();

    $scope.criteria.tagOptions = bootstrappedData.tagOptions;
    $scope.criteria.dateFrom = dateFrom;
    $scope.criteria.dateTo = new Date();
    

    if ($scope.criteria.tagOptions.length > 0) {
        $scope.criteria.tag = $scope.criteria.tagOptions[0];
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
        if ($scope.criteriaForm == undefined) {
            return;
        }
        var s = $scope.criteriaForm[name];
        return s.$invalid ? "has-error" : "";        
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
        $scope.refreshChart();
    }
});

