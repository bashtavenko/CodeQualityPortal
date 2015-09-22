'use strict';

scatterModule.factory("scatterService", function($resource) {
    return {        
        getSystems: function (dateId) {
            var params = {
                dateId: dateId                
            };
            return $resource("/api/systems/:dateId", { dateId: "@dateId" })
                .query(params);            
        }
    };
});