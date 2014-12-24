'use strict';

churnModule.controller("ChurnController", function ($scope, bootstrappedData, $resource, $log) {    
    var dateFrom = new Date();
    dateFrom.setDate(dateFrom.getDate() - 14);

    var minDate = new Date();
    minDate.setDate(minDate.getDate() - 365);
    $scope.minDate = minDate;

    $scope.maxDate = new Date();

    $scope.criteria = {
        repoOptions: bootstrappedData.repoOptions,
        dateFrom: dateFrom,
        dateTo: new Date(),
        extension: "cs"
    };

    if ($scope.criteria.repoOptions.length > 0) {
        $scope.criteria.repo = $scope.criteria.repoOptions[0];
    }
        
    $scope.init = function () {
        $scope.refresh($scope.criteria);
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
                
        var params = {
            repoId: criteria.repo.repoId,
            dateFrom: criteria.dateFrom.toISOString().slice(0, 10).replace(/-/g, '-'),
            dateTo: criteria.dateTo.toISOString().slice(0, 10).replace(/-/g, '-'),
            fileExtension: criteria.extension
        };

        // Wijmo x-axis labels
        var timeDiff = Math.abs(criteria.dateFrom.getTime() - criteria.dateTo.getTime());
        var dayDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));
        $scope.labelsVisible = dayDiff < 25;
        
        var Trend = $resource("/api/trend/:repoId/:dateFrom/:dateTo/:fileExtension",
            { repoId: "@repoId", dateFrom: "@dateFrom", dateTo: "@dateTo", fileExtension: "@fileExtension" });
            
        $scope.commits = [];
        $scope.selectedDate = null;
        $scope.topFiles = [];
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
            repoId: $scope.criteria.repo.repoId,
            dateId: $scope.trendSelection.collectionView.currentItem.dateId,
            fileExtension: $scope.criteria.extension            
        };

        var Files = $resource("/api/files/:repoId/:dateId/:fileExtension",
            { repoId: "@repoId", dateId: "@dateId", fileExtension: "@fileExtension", topX: 5 });
        Files.query(params,
            function (data) {
                $scope.topFiles = data
            },
            function (error) {
                $log.error(error);
            });

        var Commits = $resource("/api/commits/:repoId/:dateId/:fileExtension",
            { repoId: "@repoId", dateId: "@dateId", fileExtension: "@fileExtension" });

        Commits.query(params,
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    data[i].expanded = false;
                    data[i].files = [];
                }
                $scope.commits = data;
            },
            function (error) {
                $log.error(error);
            });        
    }

    $scope.commitClick = function (commit) {
        commit.expanded = !commit.expanded;        

        if (commit.files.length > 0 && commit.expanded) {            
            return;
        }
        var params = {
            commitId: commit.commitId,            
            fileExtension: $scope.criteria.extension
        };

        var Files = $resource("/api/files/:commitId/:fileExtension",
            { commitId: "@commitId", fileExtension: "@fileExtension" });

        Files.query(params,
            function (data) {
                commit.files = data;
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
        var s = $scope.repoForm[name];
        return s.$invalid ? "has-error" : "";
    }
});

