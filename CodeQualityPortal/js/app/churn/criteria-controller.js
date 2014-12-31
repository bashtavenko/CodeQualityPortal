'use strict';

churnModule.controller("CriteriaController", function ($scope, bootstrappedData, $log) {    
    var dateFrom = new Date();
    dateFrom.setDate(dateFrom.getDate() - 14);

    var minDate = new Date();
    minDate.setDate(minDate.getDate() - 365);
    $scope.minDate = minDate;

    $scope.maxDate = new Date();

    $scope.criteria.repoOptions = bootstrappedData.repoOptions;
    $scope.criteria.dateFrom = dateFrom;
    $scope.criteria.dateTo = new Date();
    $scope.criteria.extension = "cs"
    
    if ($scope.criteria.repoOptions.length > 0) {
        $scope.criteria.repo = $scope.criteria.repoOptions[0];
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
        var s = $scope.repoForm[name];
        return s.$invalid ? "has-error" : "";
    }

    $scope.refresh = function (criteria) {
        if ($scope.repoForm != undefined) {
            // Setting min and max dates don't stop from typing in dates that are out of range
            $scope.repoForm.dateFrom.$setValidity('valid', criteria.dateFrom >= $scope.minDate && criteria.dateFrom <= $scope.maxDate);
            $scope.repoForm.dateTo.$setValidity('valid', criteria.dateTo >= $scope.minDate && criteria.dateTo <= $scope.maxDate);
            if ($scope.repoForm.$invalid) {
                return;
            }
        }

        $scope.refreshChart(criteria);
    }
});

