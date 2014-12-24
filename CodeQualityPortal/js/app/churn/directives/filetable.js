'use strict';

churnModule.directive('fileTable', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/js/app/churn/templates/filetable.html',
        scope: {
            files: '=files'
        }
    }
});