jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-dmy-pre": function (a) {
        if (a == null || a == "") {
            return 0;
        }
        var date = a.split('-');
        return (date[2] + date[1] + date[0]) * 1;
    },

    "date-dmy-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "date-dmy-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});