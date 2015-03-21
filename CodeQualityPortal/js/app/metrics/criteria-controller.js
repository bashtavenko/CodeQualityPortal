'use strict';

// Populates "criteria" in parent scope (mostly)
metricsModule.controller("CriteriaController", function ($scope, bootstrappedData, metricsService) {
    $scope.dateTimePicker = {};

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

    
    $scope.loadMembers = function () {
        $scope.initSelect('memberOptions', 'member');
        if ($scope.criteria.type.id > 0) {
            metricsService.getMembers($scope.criteria.module.id, $scope.criteria.namespace.id, $scope.criteria.type.id)
              .$promise.then(function (data) {
                  $scope.memberOptions = $scope.memberOptions.concat(data);
              });
        }
    };

    $scope.loadTypes = function () {
        $scope.initSelect('typeOptions', 'type');
        if ($scope.criteria.namespace.id > 0) {
            metricsService.getTypes($scope.criteria.module.id, $scope.criteria.namespace.id)
              .$promise.then(function (data) {
                  $scope.typeOptions = $scope.typeOptions.concat(data);
              });
        }
        $scope.loadMembers();
    };    

    $scope.loadNamespaces = function () {
        $scope.initSelect('namespaceOptions', 'namespace');
        if ($scope.criteria.module.id > 0) {
            metricsService.getNamespaces($scope.criteria.module.id)
              .$promise.then(function (data) {
                  $scope.namespaceOptions = $scope.namespaceOptions.concat(data);
              });
        }
        $scope.loadTypes();
    };

    $scope.loadModules = function () {
        $scope.initSelect('moduleOptions', 'module');        
        metricsService.getModules(null)
              .$promise.then(function (data) {
                  $scope.moduleOptions = $scope.moduleOptions.concat(data);
             });        
        $scope.loadNamespaces();
    };
    

    $scope.moduleChanged = function () {
        $scope.loadNamespaces();
        if ($scope.criteria.module.id == -1) {
            $scope.setMode(new $scope.systemMode);            
        }
        else {            
            $scope.setMode(new $scope.moduleMode);
        }
        $scope.refresh();
    };

    $scope.namespaceChanged = function () {
        $scope.loadTypes();
        if ($scope.criteria.namespace.id == -1) {
            $scope.setMode(new $scope.moduleMode);
        }
        else {
            $scope.setMode(new $scope.namespaceMode);
        }
        $scope.refresh();
    };

    $scope.typeChanged = function () {
        $scope.loadMembers();
        if ($scope.criteria.type.id == -1) {
            $scope.setMode(new $scope.namespaceMode);
        }
        else {
            $scope.setMode(new $scope.typeMode);
        }
        $scope.refresh();
    };

    $scope.memberChanged = function () {        
        if ($scope.criteria.member.id == -1) {
            $scope.setMode(new $scope.typeMode);
        }
        else {
            $scope.setMode(new $scope.memberMode);
        }
        $scope.refresh();
    };
    
    $scope.refresh = function () {
        if ($scope.criteriaForm != undefined) {
            // Setting min and max dates don't stop from typing in dates that are out of range
            var dateFromValid = $scope.criteria.dateFrom >= $scope.minDate && $scope.criteria.dateFrom <= $scope.maxDate;
            var dateToValid = $scope.criteria.dateTo >= $scope.minDate && $scope.criteria.dateTo <= $scope.maxDate;
            $scope.criteriaForm.dateFrom.$setValidity('valid', dateFromValid);
            $scope.criteriaForm.dateTo.$setValidity('valid', dateToValid);
            if (!dateFromValid || !dateToValid) {
                return;
            }
        }
        $scope.refreshChart();
    }

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
        if ($scope.criteriaForm == undefined) {
            return;
        }
        var s = $scope.criteriaForm[name];
        return s.$invalid ? "has-error" : "";
    }

    $scope.loadModules(null);
});

