'use strict';

churnModule.directive('linesAdded', function () {
    return {
        restrict: 'E',
        replace: true,
        template: "<span class='lines-added'>&plus;{{lines|number}}</span>",
        scope: {
            lines:"="
        }
    }
});