'use strict';

metricsModule.factory("metricsService", function($resource) {
    return {
        getModules: function (tag) {
            var params = {
                tag: tag                
            };
            return $resource("/api/modules/:tag", { tag: "@tag" }).query(params);
        }
    };
});