'use strict';

/// <reference path="churn-controller.js" />

churnModule.controller("ChurnController", function ($scope, bootstrappedData) {    
    var dateFrom = new Date();
    dateFrom.setDate(dateFrom.getDate() - 14);
    $scope.criteria = {
        repoOptions: bootstrappedData.repoOptions,
        dateFrom: dateFrom,
        dateTo: new Date(),
        extension:"cs"
    };
    if ($scope.criteria.repoOptions.length > 0) {
        $scope.criteria.repo = $scope.criteria.repoOptions[0];
    }

    $scope.refresh = function(criteria) {
        console.log(criteria);
    }
});