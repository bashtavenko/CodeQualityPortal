'use strict';

churnModule.directive('spinner', function () {
    return {
        restrict: 'E',
        template: "<div id='spinner' style='display:none;margin-top:15px;'><strong>Loading..</strong></div>"
    }
});