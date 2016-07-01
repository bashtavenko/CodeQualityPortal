'use strict';

churnModule.controller("CoverageController", function ($scope, bootstrappedData, coverageService, $log) {
    $scope.data = bootstrappedData.root;
    $scope.summaryBy = 0;

    $scope.summaryByList = [
        { id: 0, name: "System" },
        { id: 1, name: "Repo" },
        { id: 2, name: "Team" }
    ];

    $scope.refresh = function () {
        coverageService.getCoverage($scope.summaryBy)
            .then(function(response) {
                $scope.data = response;
            })
            .catch(function(err) {
                $log.error(err);
            });
    }
});

