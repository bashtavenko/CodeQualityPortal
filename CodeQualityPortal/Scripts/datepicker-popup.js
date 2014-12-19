// This is needed in order to correctly initialize date time picker for UI bootstrap
// https://github.com/mgcrea/angular-strap/issues/1154
// Can probably be removed in the future

angular.module('datepicker-popup', [])
.directive('datepickerPopup', function () {
    return {
        restrict: 'EAC',
        require: 'ngModel',
        link: function (scope, element, attr, controller) {
            //remove the default formatter from the input directive to prevent conflict
            controller.$formatters.shift();
        }
    }
});