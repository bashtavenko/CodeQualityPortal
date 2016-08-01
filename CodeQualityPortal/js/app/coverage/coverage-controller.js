'use strict';

churnModule.controller("CoverageController", function ($scope, bootstrappedData, coverageService, $log) {
    $scope.data = bootstrappedData.root;
    $scope.summaryBy = 0;
    $scope.moduleStats = [];
    $scope.chartProps = {
        selection: null
    };

    $scope.summaryByList = [
        { id: 0, name: "System" },
        { id: 1, name: "Repo" },
        { id: 2, name: "Team" }
    ];

    $scope.refresh = function () {
        coverageService.getCoverage($scope.summaryBy)
            .then(function(response) {
                $scope.data = response;
                $scope.moduleStats = [];
            })
            .catch(function(err) {
                $log.error(err);
            });
    }

    $scope.dateClicked = function(categoryId, itemTmp) {
        if ($scope.chartProps.selection != null) {
            // TODO: It may be possible to get current item through $item similar to grid instead of going through chartProps
            var item = $scope.chartProps.selection.collectionView.currentItem;
            coverageService.getModuleStats($scope.summaryBy, categoryId, item.dateId)
                .then(function(response) {
                    $scope.addModuleStats(categoryId, item.dateString, response);
                })
                .catch(function(err) {
                    $log.error(err);
                });
        }
    }

    $scope.addModuleStats = function (categoryId, date, data) {
        var index = $scope.getModuleStatsIndex(categoryId);
        if (index >= 0) {
            $scope.moduleStats[index].data = data;
            $scope.moduleStats[index].date = date;
        } else {
            $scope.moduleStats.push({ categoryId: categoryId, date: date, data: data });
        }
    }

    $scope.getModuleStatsIndex = function (categoryId) {
        var index = -1;
        for (var i = 0; i < $scope.moduleStats.length; i++) {
            var item = $scope.moduleStats[i];
            if (item.categoryId === categoryId) {
                index = i;
                break;
            }
        }
        return index;
    }

    $scope.getModuleStats = function (categoryId) {
        var index = $scope.getModuleStatsIndex(categoryId);
        return index >= 0 ? $scope.moduleStats[index] : null;
    }
});

