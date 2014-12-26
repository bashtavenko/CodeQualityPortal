'use strict';

metricsModule.controller("MetricsController", function ($scope, bootstrappedData, $resource, $log) {    
    $scope.criteria = {}; // shared with child controllers   
                    
    $scope.init = function () {        
        $scope.refreshChart();
    }    

    $scope.refreshChart = function () {        
        var params = {
            tag: $scope.criteria.tag,
            dateFrom: $scope.criteria.dateFrom.toISOString().slice(0, 10).replace(/-/g, '-'),
            dateTo: $scope.criteria.dateTo.toISOString().slice(0, 10).replace(/-/g, '-')
        };
                
        var Trend = $resource("/api/moduletrend/:tag/:dateFrom/:dateTo",
            { tag: "@tag", dateFrom: "@dateFrom", dateTo: "@dateTo" });
        
        Trend.query(params,
            function (data) {
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
        var params = {
            tag: $scope.criteria.tag,
            dateId: $scope.criteria.selectedDate.dateId
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
});

