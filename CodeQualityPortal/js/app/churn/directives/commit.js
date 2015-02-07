'use strict';

churnModule.directive('commit', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/js/app/churn/templates/commit.html',
        scope: {
            commit: '=',
            method: '&' // method should not have parenthesis
        },
        controller: function($scope) {
            $scope.innerCommitClick = function() {
                $scope.method()($scope.commit);
            }
        }
    }
});