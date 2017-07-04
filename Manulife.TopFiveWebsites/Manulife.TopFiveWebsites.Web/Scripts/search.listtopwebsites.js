$(document).ready(function () {
    //init datatable
	$("#top-results-grid").DataTable({
		processing: true,
        serverSide: true,
        language: {
            lengthMenu: "Top _MENU_ Websites",
            search: "As Of"
        },
        lengthMenu: [[1, 2, 3, 4, 5, 6, 7, 8, 9, 10], ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10"]],
        pageLength: 5,
        //dom: "<'row'<'col-sm-6'l><'col-sm-5'f><'#searchDatePicker'>> <'row'<'col-sm-12'tr>> <'row'<'col-sm-5'i><'col-sm-7'p>>",
        searching: true,
        search: {
            search: moment().format("YYYY-MM-DD")
        },
        searchDelay: 700,
        ajax: "/Search/GetTopWebsites",
        columns: [
            { data: "Date", type: "date", render: app.renderDate, orderable: false },
            { data: "Website" },
            { data: "TotalVisits", type: "num-fmt", render: app.renderNumber }
        ],
        orderMulti: false,
        order: [[2, 'desc']]
    });

    //init date picker to search input box
    var searchInput = $("#top-results-grid_filter input");
    searchInput.prop("readonly", true);
    searchInput.datetimepicker({
        format: "YYYY-MM-DD",
        ignoreReadonly: true
    }).on("dp.change", function (e) {
        searchInput.keyup();
    });
});