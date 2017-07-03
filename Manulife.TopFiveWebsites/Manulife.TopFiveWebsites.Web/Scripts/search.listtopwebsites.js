$(document).ready(function () {
	$("#top-results-grid").DataTable({
		processing: true,
        serverSide: true,
        language: {
            lengthMenu: "Top _MENU_ Websites"
        },
        lengthMenu: [[3, 4, 5, 6, 7, 8, 9, 10], ["3", "4", "5", "6", "7", "8", "9", "10"]],
        pageLength: 5,
        //dom: "<'row'<'col-sm-6'l><'col-sm-5'f><'#searchDatePicker'>> <'row'<'col-sm-12'tr>> <'row'<'col-sm-5'i><'col-sm-7'p>>",
        searching: true,
        searchDelay: 700,
        ajax: "/Search/GetTopWebsites",
        columns: [
            { data: "Date", type: "date", render: app.renderDate },
            { data: "Website" },
            { data: "TotalVisits", type: "num-fmt", render: app.renderNumber }
        ]
    });
    var searchInput = $("#top-results-grid_filter input");
});