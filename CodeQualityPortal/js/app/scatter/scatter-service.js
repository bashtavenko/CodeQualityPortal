'use strict';

scatterModule.factory("scatterService", function($resource) {
    return {        
        getSystems: function (dateId) {
            var params = {
                dateId: dateId                
            };
            return $resource("/api/systems/:dateId", { dateId: "@dateId" })
                .query(params);
        },
        getModulesByDate: function (systemId, dateId) {
            var params = {
                dateId: dateId,
                systemId: systemId
            };
            return $resource("/api/modules/:dateId/:systemId", { dateId: "@dateId", systemId: "@systemId", })
                .query(params);
        },
    };
});