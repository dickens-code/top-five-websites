var app = {
    renderDate: function (data, type, row, meta) {
        if (!data)
            return "N/A";

        return moment(data).format("Do MMM YYYY");
    },
    renderNumber: function (data, type, row, meta) {
        if (!data)
            return "N/A";

        return $.number(data);
    }
};