$(document).ready(function () {
	$('#top-results-grid').DataTable({
		processing: true,
        serverSide: true,
        lengthMenu: [[3, 4, 5, 6, 7, 8, 9, 10], ["3", "4", "5", "6", "7", "8", "9", "10"]],
        pageLength: 5,
        searching: true,
        searchDelay: 700,
        ajax: "/Search/GetTopWebsites",
        columns: [
            { data: "Date" },
            { data: "Website" },
            { data: "TotalVisits" },
        ]
	});
});