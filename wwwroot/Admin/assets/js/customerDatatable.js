$(document).ready(function () {
    $("#customerDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/Users/GetUserData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "place.name", "name": "job name", "autoWidth": true },
            { "data": "phone.number", "name": "phone", "autoWidth": true },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            { "data": "userStatus.name", "name": "createdAt", "autoWidth": true },
            {
                "render": function (data,row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id+ "'); >Delete</a>";   }
            },
        ]
    });
});  