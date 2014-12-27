'use strict';

metricsModule.factory("metricsService", function($resource) {
    return {
        getModules: function (tag) {
            var params = {
                tag: tag                
            };
            return $resource("/api/modules/:tag", { tag: "@tag" })
                .query(params);
        },
        getTagTrend: function (tag, dateFrom, dateTo) {
            var params = {
                tag: tag,
                dateFrom: dateFrom.toISOString().slice(0, 10).replace(/-/g, '-'),
                dateTo: dateTo.toISOString().slice(0, 10).replace(/-/g, '-')
            };

            return $resource("/api/moduletrend/:tag/:dateFrom/:dateTo",
                { tag: "@tag", dateFrom: "@dateFrom", dateTo: "@dateTo" })
                .query(params);
        },
        getModulesByDate: function (tag, dateId) {
            var params = {
                tag: tag,
                dateId: dateId
            };
            return $resource("/api/modules/:tag/:dateId", { tag: "@tag", dateId: "@dateId" })
                .query(params);
        }
    };
});