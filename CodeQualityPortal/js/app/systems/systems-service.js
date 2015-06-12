'use strict';

systemsModule.factory("systemsService", function($resource) {
    return {        
        getStats: function () {
            return $resource("/api/systemstats").query();
        }
    };
});