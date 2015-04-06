'use strict';

churnModule.controller("GridController", function ($scope, churnService) {
    
    $scope.repoClicked = function (item) {
        $scope.$parent.repo = item.repoName;
        $scope.$parent.topFiles = churnService.getFiles(item.repoId, $scope.selectedDate.dateId, 5);

        churnService.getCommits(item.repoId, $scope.selectedDate.dateId)
            .$promise.then(function (data) {
                for (var i = 0; i < data.length; i++) {
                    data[i].expanded = false;
                    data[i].files = [];
                }                
                $scope.$parent.commits = data;                
            });        
    }
});

