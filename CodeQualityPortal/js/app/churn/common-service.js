﻿'use strict';

churnModule.factory("commonService", function () {
    return {
        convertDateToIso: function (date) {
            return date.toISOString().slice(0, 10).replace(/-/g, '-');

        }
    }
});