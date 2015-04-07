var churnModule = angular.module("churnModule", ['ngResource', 'wj', 'ui.bootstrap', 'datepicker-popup']);
var metricsModule = angular.module("metricsModule", ['ngResource', 'wj', 'ui.bootstrap', 'datepicker-popup']);


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
        'response': function(response) {
            $("#spinner").hide();
            return response;
        },
        'requestError': function(rejection) {
            $("#spinner").hide();
            return $q.reject(rejection);
        }
    };
});
