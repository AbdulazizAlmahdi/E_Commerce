showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            $("#form-modal .modal-body").html(data);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal("show");
        }
    })
}
$(document).ready(function () {
    userDatatable();
    helpDatatable();
});
userDatatable = () => {
    $("#userDatatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        paging: true,
        lengthChange: true,
        searching: true,
        ordering: true,
        lengthMenu: [7, 10, 25, 50, 75, 100],
        responsive: false,
        dom:
            '<"row"<"col-sm-12"<"col-sm-12"B>>>' + '<"row"<"col-sm-12 col-md-6"l>' + '<"col-sm-12 col-md-6"f>>' +
            '<"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        buttons: [
            {
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة مستخدم</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Admin/Users/CreateOrEdit', 'تعديل المستخدم');
                },
            },
            {
                extend: 'collection',
                className: 'class="dt-button buttons-collection btn btn-label-primary dropdown-toggle me-2"',
                text: 'تصدير',
                buttons: [
                    {
                        extend: 'copy',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'Copy',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },

                    {
                        extend: 'pdf',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'PDF',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'excel',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'Excel',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'CSV',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'Print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    }




                ]
            }

        ],
        ajax: {
            "url": "/Admin/Users/GetUserData",
            "type": "POST",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": true,
            "searchable": true
        }],
        columns: [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "place.name", "name": "job name", "autoWidth": true, "orderable": false },
            { "data": "phone.number", "name": "phone", "autoWidth": true, "orderable": false },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            { "data": "status", "name": "Status", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/Users/CreateOrEdit/' + ${row.id}, 'تعديل المستخدم')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/Users/Delete/' + ${row.id}, 'حذف المستخدم')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}
helpDatatable = () => {
    $("#help-datatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        paging: true,
        lengthChange: true,
        searching: true,
        ordering: true,
        lengthMenu: [7, 10, 25, 50, 75, 100],
        responsive: false,
        dom:
            '<"row"<"col-sm-12"<"col-sm-12"B>>>' + '<"row"<"col-sm-12 col-md-6"l>' + '<"col-sm-12 col-md-6"f>>' +
            '<"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        buttons: [
            {
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة مساعدة</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Admin/Help/CreateAndEdit', 'إضافة المساعدة');
                },
            },
            {
                extend: 'collection',
                className: 'class="dt-button buttons-collection btn btn-label-primary dropdown-toggle me-2"',
                text: 'تصدير',
                buttons: [
                    {
                        extend: 'copy',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'Copy',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },

                    {
                        extend: 'pdf',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'PDF',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'excel',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'Excel',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'CSV',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'Print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    }
                ]
            }

        ],
        ajax: {
            "url": "/Admin/Help/GetHelpData",
            "type": "POST",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": true,
            "searchable": true
        },
        {
            "targets": [1],
            render: function (data, type, row) {
                switch (data) {
                    case "Solved": return 'تم حل المشكلة'; break;
                    case "Unsolved": return 'تم تجاهل المشكلة'; break;
                    default: return 'لم يتم حل المشكلة'; break;
                }
            }
        },
        ],
        columns: [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "status", "name": "Status", "autoWidth": true, "orderable": true },
            { "data": "subject", "name": "Subject", "autoWidth": true, "orderable": false },
            { "data": "phone.number", "name": "phone", "autoWidth": true, "orderable": false },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/Help/CreateAndEdit/' + ${row.id}, 'عرض الطلب')" class="btn btn-primary btn-sm">عرض الطلب</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/Help/SolveOrder/' + ${row.id}, 'حل الطلب')" class="btn btn-primary btn-sm">حل الطلب</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/Help/IgnoreOrder/' + ${row.id}, 'تجاهل الطلب')" class="btn btn-primary btn-sm">تجاهل الطلب</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.status == "success") {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    if (res.type == "help")
                        helpDatatable();
                    else if (res.type == "user")
                        userDatatable();
                    $('#success-toast .success-toast-title').html(res.messgaeTitle);
                    $('#success-toast .success-toast-body').html(res.messageBody);
                    new bootstrap.Toast(document.getElementById('success-toast')).show();
                }
                else if (res.status == "validation-error") {
                    $('#form-modal .modal-body').html(res.html);
                }
                else {
                    console.log("Error");
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    if (res.type == "help")
                        helpDatatable();
                    else if (res.type == "user")
                        userDatatable();
                    $('#error-toast .error-toast-title').html(res.messgaeTitle);
                    $('#error-toast .error-toast-body').html(res.messageBody);
                    new bootstrap.Toast(document.getElementById('error-toast')).show();
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}
userDatatable = () => {
    $("#category-datatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        paging: true,
        lengthChange: true,
        searching: true,
        ordering: true,
        lengthMenu: [7, 10, 25, 50, 75, 100],
        responsive: false,
        dom:
            '<"row"<"col-sm-12"<"col-sm-12"B>>>' + '<"row"<"col-sm-12 col-md-6"l>' + '<"col-sm-12 col-md-6"f>>' +
            '<"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        buttons: [
            {
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة مستخدم</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Admin/Users/CreateOrEdit', 'تعديل المستخدم');
                },
            },
            {
                extend: 'collection',
                className: 'class="dt-button buttons-collection btn btn-label-primary dropdown-toggle me-2"',
                text: 'تصدير',
                buttons: [
                    {
                        extend: 'copy',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'Copy',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },

                    {
                        extend: 'pdf',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'PDF',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'excel',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'Excel',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'CSV',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        className: 'dt-button buttons-print dropdown-item',
                        text: 'Print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    }




                ]
            }

        ],
        ajax: {
            "url": "/Admin/Category/GetCategoryData",
            "type": "POST",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": true,
            "searchable": true
        }],
        columns: [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/Users/CreateOrEdit/' + ${row.id}, 'تعديل المستخدم')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/Users/Delete/' + ${row.id}, 'حذف المستخدم')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}
// jQueryAjaxDelete = form => {
//     if (confirm('Are you sure to delete this record ?')) {
//         try {
//             $.ajax({
//                 type: 'POST',
//                 url: form.action,
//                 data: new FormData(form),
//                 contentType: false,
//                 processData: false,
//                 success: function (res) {
//                     $('#view-all').html(res.html);
//                 },
//                 error: function (err) {
//                     console.log(err)
//                 }
//             })
//         } catch (ex) {
//             console.log(ex)
//         }
//     }

//     //prevent default form submit event
//     return false;
// }

