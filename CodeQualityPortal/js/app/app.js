// TODO: Either have one module or find a way to have interceptor in each of them
var churnModule = angular.module("churnModule", ['ngResource', 'wj', 'ui.bootstrap', 'datepicker-popup']);
var metricsModule = angular.module("metricsModule", ['ngResource', 'wj', 'ui.bootstrap', 'datepicker-popup']);
var systemsModule = angular.module("systemsModule", ['ngResource', 'wj', 'ui.bootstrap']);
var scatterModule = angular.module("scatterModule", ['ngResource', 'wj', 'ui.bootstrap']);
var branchDiffModule = angular.module("branchDiffModule", ['ngResource', 'wj', 'ui.bootstrap']);

churnModule.config(function ($httpProvider) {
    $httpProvider.interceptors.push('myInterceptor');
    
    var spinner = function spinnerFunction(data, headersGetter) {
        $("#spinner").show();
        return data;
    };    

    $httpProvider.defaults.transformRequest.push(spinner);
});

churnModule.factory('myInterceptor', function($q) {
    return {
        'response': function (response) {
            $("#spinner").hide();
            return response;
        },
        'responseError': function (rejection) {
            $("#spinner").text("Error occured. See details in server log.");
            $("#spinner").css("color", '#ff0906');
            return $q.reject(rejection);
        }
    };
});
