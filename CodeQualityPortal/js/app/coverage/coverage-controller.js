'use strict';

coverageModule.controller("CoverageController", function ($scope, bootstrappedData, coverageService, $log) {
    $scope.data = bootstrappedData.root;
    $scope.summaryBy = 0;

    $scope.summaryByChanged = function () {
        coverageService.getCoverage($scope.summaryBy)
            .then(function(response) {
                $scope.data = response;
            })
            .catch(function(err) {
                $log.error(err);
            });
    }
});

