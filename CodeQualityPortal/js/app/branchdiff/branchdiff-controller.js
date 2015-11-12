'use strict';

branchDiffModule.controller("BranchDiffController", function ($scope, bootstrappedData, $log, branchDiffService) {
    $scope.criteria = {};
    $scope.masterOption = { id: -1, nameAndDate: "", name: "master", createdDate: "" };

    $scope.branches = [$scope.masterOption];
    $scope.branches = $scope.branches.concat(bootstrappedData.branches);
    
    $scope.criteria.branchA = $scope.branches[0];
    $scope.criteria.branchB = $scope.branches.length > 1 ? $scope.branches[1] : $scope.branches[0];
    
    $scope.branchAChanged = function () {
        $scope.setBranchADates();
    };

    $scope.branchBChanged = function () {
        $scope.setBranchBDates();
    };

    $scope.setBranchADates = function (){
        $scope.setBranchDates($scope.criteria.branchA.id, "branchADates", "dateA");
    }

    $scope.setBranchBDates = function () {
        $scope.setBranchDates($scope.criteria.branchB.id, "branchBDates", "dateB");
    }

    $scope.setBranchDates = function (branchId, optionsProperty, selectedItemProperty) {
        branchDiffService.getDates(branchId)
            .$promise.then(
                function (data) {
                    $scope[optionsProperty] = data;
                    if (data.length > 0) {
                        $scope.criteria[selectedItemProperty] = data[0];
                    }
                }
            );
    }

    $scope.canShow = function() {
        return $scope.criteria.branchA.id != $scope.criteria.branchB.id;
    }

    $scope.getData = function() {
        var branchA = $scope.criteria.branchA.id;
        var branchB = $scope.criteria.branchB.id;
        var dateA = $scope.criteria.dateA.dateId;
        var dateB = $scope.criteria.dateB.dateId;
        branchDiffService.getData(branchA, dateA, branchB, dateB)
            .$promise.then(
                function(data) {
                    $scope.summaryItems = data.diffSummaryItems;
                    $scope.items = data.moduleDiff;
                }
            );
    }

    $scope.setBranchADates();
    $scope.setBranchBDates();
});