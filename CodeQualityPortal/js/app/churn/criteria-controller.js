'use strict';

churnModule.controller("CriteriaController", function ($scope, bootstrappedData, $log) {
    $scope.dateTimePicker = {};
    var dateFrom = new Date();
    dateFrom.setDate(dateFrom.getDate() - 14);

    var minDate = new Date();
    minDate.setDate(minDate.getDate() - 365);
    $scope.minDate = minDate;

    $scope.maxDate = new Date();

    $scope.repoOptions = bootstrappedData.repoOptions;
    $scope.criteria.dateFrom = dateFrom;
    $scope.criteria.dateTo = new Date();
    $scope.criteria.extension = "cs"
    
    if ($scope.repoOptions.length > 0) {
        $scope.criteria.repo = $scope.repoOptions[0];
    }

    $scope.repoChanged = function () {
        $scope.refreshChart($scope.criteria);
    };
        
    $scope.openDateFrom = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.dateTimePicker.dateFromOpened = true;
    };

    $scope.openDateTo = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.dateTimePicker.dateToOpened = true;
    };

    $scope.error = function (name) {
        var s = $scope.repoForm[name];
        return s.$invalid ? "has-error" : "";
    }

    $scope.refresh = function (criteria) {
        if ($scope.repoForm != undefined) {
            // Setting min and max dates don't stop from typing in dates that are out of range
            var dateFromValid = criteria.dateFrom >= $scope.minDate && criteria.dateFrom <= $scope.maxDate;
            var dateToValid = criteria.dateTo >= $scope.minDate && criteria.dateTo <= $scope.maxDate;
            $scope.repoForm.dateFrom.$setValidity('valid', dateFromValid);
            $scope.repoForm.dateTo.$setValidity('valid', dateToValid);
            if (!dateFromValid || !dateToValid) {
                return;
            }
        }

        $scope.refreshChart(criteria);
    }
});

