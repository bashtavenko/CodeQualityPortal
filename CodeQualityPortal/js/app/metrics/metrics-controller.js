'use strict';

metricsModule.controller("MetricsController", function ($scope, bootstrappedData, $log, metricsService) {    
    $scope.criteria = {}; // shared with child controllers       
    $scope.mode = {}; // shared with child controllers       
                        
    $scope.init = function () {
        $scope.mode = new $scope.systemMode;
        $scope.refreshChart();
    }
    
    // Can't set mode from child scope without this
    $scope.setMode = function (mode) {
        $scope.mode = mode;    
    };

    $scope.refreshChart = function () {
        $scope.mode.getTrend()
            .$promise.then(
                function(data) {
                    $scope.criteria.selectedDate = null;
                    $scope.items = [];
                    $scope.$broadcast("adjust_chart");
                    $scope.trendData = data;
                },
                function (error) {
                    $log.error(error);
                });
    }

    $scope.refreshGrid = function () {
        // Can't bind directly to items since wijmo doesn't like promise
        $scope.mode.getItems()        
            .$promise.then(
                function (data) {
                    $scope.items = data;
                }
        );
    }

    // 1 - system
    $scope.systemMode = function () {
        return {
            getTrend: function() {
                return metricsService.getModuleTrend($scope.criteria.system, $scope.criteria.dateFrom, $scope.criteria.dateTo);
            },
            getItems: function() {
                return metricsService.getModulesByDate($scope.criteria.system.id, $scope.criteria.selectedDate.dateId);
            },
            trendLabel: $scope.criteria.system.id == -1 ? "All systems trend" : "System trend for " + $scope.criteria.system.name,
            itemsLabel: "Modules on ",
            chartSelection: "Point"
        };
    };

    // 2 - module
    $scope.moduleMode = function () {
        return {
            getTrend: function () {
                return metricsService.getNamespaceTrend($scope.criteria.module.id, $scope.criteria.dateFrom, $scope.criteria.dateTo);
            },
            getItems: function () {
                return metricsService.getNamespacesByDate($scope.criteria.module.id, $scope.criteria.selectedDate.dateId);
            },
            trendLabel: "Module trend for " + $scope.criteria.module.name,
            itemsLabel: "Namespaces on ",
            chartSelection: "Point"
        };
    };

    // 3 - namespace
    $scope.namespaceMode = function () {
        return {
            getTrend: function () {
                return metricsService.getTypeTrend($scope.criteria.module.id, $scope.criteria.namespace.id, $scope.criteria.dateFrom, $scope.criteria.dateTo);
            },
            getItems: function () {
                return metricsService.getTypesByDate($scope.criteria.module.id, $scope.criteria.namespace.id, $scope.criteria.selectedDate.dateId);
            },
            trendLabel: "Namespace trend for " + $scope.criteria.namespace.name,
            itemsLabel: "Types on ",
            chartSelection: "Point"
        };
    };

    // 4 - type
    $scope.typeMode = function () {
        return {
            getTrend: function () {
                return metricsService.getMemberTrend($scope.criteria.module.id, $scope.criteria.namespace.id, $scope.criteria.type.id, $scope.criteria.dateFrom, $scope.criteria.dateTo);
            },
            getItems: function () {
                return metricsService.getMembersByDate($scope.criteria.module.id, $scope.criteria.namespace.id, $scope.criteria.type.id, $scope.criteria.selectedDate.dateId);
            },
            trendLabel: "Type trend for " + $scope.criteria.type.name,
            itemsLabel: "Members on ",
            chartSelection: "Point"
        };
    };

    // 5 - member
    $scope.memberMode = function () {
        return {
            getTrend: function () {
                return metricsService.getSingleMemberTrend($scope.criteria.module.id, $scope.criteria.namespace.id, $scope.criteria.type.id, $scope.criteria.member.id, $scope.criteria.dateFrom, $scope.criteria.dateTo);
            },
            getItems: function () {},
            trendLabel: "Member trend for " + $scope.criteria.member.name,
            chartSelection: "None"
        };
    };
});

