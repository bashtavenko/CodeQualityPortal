/// <reference path="churn-controller.js" />
churnModule.controller("ChurnController", function ($scope, bootstrappedData) {
    $scope.repoOptions = bootstrappedData.repoOptions;
    if ($scope.repoOptions.length > 0) {
        $scope.repo = $scope.repoOptions[0].repoId;
    }
});