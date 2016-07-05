'use strict';

churnModule.controller("ModulesLookupController", function($scope, bootstrappedData, maintenanceService, $log) {
    $scope.list = bootstrappedData.root;
    $scope.data = [];

    $scope.categoryList = [
        { id: 0, name: "System" },
        { id: 1, name: "Repo" },
        { id: 2, name: "Team" }
    ];

    $scope.category = $scope.categoryList[0];
    $scope.unassigned = false;

    // We could ng-init="categoryItem=list[0]" but that works only one time and not when category changes
    $scope.selectCategoryItem = function() {
        if ($scope.list.length > 0) {
            $scope.categoryItem = $scope.list[0];
        };
    }

    $scope.selectCategoryItem();

    $scope.categoryChanged = function() {
        maintenanceService.getList($scope.category.id)
            .then(function(response) {
                    $scope.list = response;
                    $scope.selectCategoryItem();
                }
            ).catch(function(e) {
                $log.error(e);
            });
    };

    $scope.lookup = function() {
        maintenanceService.getModules($scope.category.id, $scope.unassigned ? null : $scope.categoryItem.id)
            .then(function(response) {
                $scope.data = new wijmo.collections.CollectionView(response);

                // Default sort column
                var sd = new wijmo.collections.SortDescription('name', false);
                $scope.data.sortDescriptions.push(sd);
            })
            .catch(function(e) {
                $log.error(e);
            });
    }
});

