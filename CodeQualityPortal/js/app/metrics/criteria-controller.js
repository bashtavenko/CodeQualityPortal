'use strict';

// Populates "criteria" in parent scope (mostly)
metricsModule.controller("CriteriaController", function ($scope, bootstrappedData, metricsService) {
    
    var dateFrom = new Date();
    dateFrom.setDate(dateFrom.getDate() - 14);
    var minDate = new Date();
    minDate.setDate(minDate.getDate() - 365);
    $scope.minDate = minDate;
    $scope.maxDate = new Date();
    $scope.criteria.dateFrom = dateFrom;
    $scope.criteria.dateTo = new Date();

    $scope.allOption = { id: -1, name: "(All)" };

    $scope.initSelect = function (options, model) {
        $scope[options] = [$scope.allOption];
        $scope.criteria[model] = $scope[options][0];
    }

    $scope.initSelect('moduleOptions', 'module');
    $scope.initSelect('namespaceOptions', 'namespace');

    $scope.loadModules = function () {
        $scope.initSelect('moduleOptions', 'module');        
        metricsService.getModules($scope.criteria.tag)
          .$promise.then(function (data) {              
              $scope.moduleOptions = $scope.moduleOptions.concat(data);              
          });
    };

    $scope.loadNamespaces = function () {
        $scope.initSelect('namespaceOptions', 'namespace');
        metricsService.getNamespaces($scope.criteria.module.id)
          .$promise.then(function (data) {              
              $scope.namespaceOptions = $scope.namespaceOptions.concat(data);          
          });
    };

    $scope.criteria.tagOptions = bootstrappedData.tagOptions;
    if ($scope.criteria.tagOptions.length > 0) {
        $scope.criteria.tag = $scope.criteria.tagOptions[0];
        $scope.moduleOptions = [$scope.allOption];
        $scope.loadModules();        
    }
        
    $scope.tagChanged = function () {
        $scope.loadModules();
        $scope.initSelect('namespaceOptions', 'namespace');
        $scope.setMode(new $scope.tagMode);        
        $scope.refresh();
    };    

    $scope.moduleChanged = function () {
        $scope.loadNamespaces();
        if ($scope.criteria.module.id == -1) {
            $scope.setMode(new $scope.tagMode);            
        }
        else {            
            $scope.setMode(new $scope.moduleMode);
        }
        $scope.refresh();
    };

    $scope.namespaceChanged = function () {        
        if ($scope.criteria.namespace.id == -1) {
            $scope.setMode(new $scope.moduleMode);
        }
        else {
            $scope.setMode(new $scope.namespaceMode);
        }
        $scope.refresh();
    };
    
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
});

