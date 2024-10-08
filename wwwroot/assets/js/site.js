﻿showInPopup = (url, title) => {
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
    categoryDatatable();
    AssociationcategoryDatatable();
    productDatatable();
    AssociationproductDatatable();
    auctionDatatable();
    AssociationauctionDatatable();
    purchaseDatatable();
    AssociationpurchaseDatatable();
    farmerDatatable();
    AssociationfarmerDatatable();

});
userDatatable = () => {
    $("#userDatatable").DataTable({
        processing: true,
        serverSide: true,
        destroy: true,
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
                    showInPopup('/Admin/Users/CreateOrEdit', 'إضافة مستخدم');
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
                            columns: ':invisible'
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
            { "data": "jobName", "name": "job name", "autoWidth": true, "orderable": false },
            { "data": "directorate.governorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "directorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "phone.number", "name": "phone", "autoWidth": true, "orderable": false },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            { "data": "status", "name": "Status", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/Users/CreateOrEdit/' + ${row.id}, 'تعديل المستخدم')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
           
                        `<button onClick="showInPopup('/Admin/Users/ActiveUser/' + ${row.id}, 'تفعيل/ايقاف المستخدم')" class="btn btn-info btn-sm">تفعيل/ايقاف</button><button onClick="showInPopup('/Admin/Users/Delete/' + ${row.id}, 'حذف المستخدم')" class="btn btn-danger btn-sm">حذف</button>`;
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
        order: [[1, 'asc']],
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
                    showInPopup('/Admin/Help/CreateOrEdit', 'إضافة المساعدة');
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
            { "data": "phone", "name": "phone", "autoWidth": true, "orderable": false },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/Help/CreateOrEdit/' + ${row.id}, 'عرض الطلب')" class="btn btn-primary btn-sm">عرض الطلب</button>` +
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
categoryDatatable = () => {
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
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة تصنيف</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Admin/Category/CreateOrEdit', 'تعديل تصنيف');
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
                    return `<button onClick="showInPopup('/Admin/Category/CreateOrEdit/' + ${row.id}, 'تعديل الصنف')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/Category/Delete/' + ${row.id}, 'حذف الصنف')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}
productDatatable = () => {
    $("#productDatatable").DataTable({
        processing: true,
        serverSide: true,
        destroy: true,
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
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة منتج</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Admin/Products/CreateOrEdit', 'تعديل المنتج');
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
            "url": "/Admin/Products/GetProductData",
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
            { "data": "nameAr", "name": "nameAr", "autoWidth": true },
            { "data": "category.name", "name": "Name", "autoWidth": true },
            { "data": "directorate.governorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "directorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "quantity", "name": "Name", "autoWidth": true },
            { "data": "unit", "name": "Name", "autoWidth": true },
            { "data": "price", "name": "Name", "autoWidth": true },
            { "data": "status", "name": "Name", "autoWidth": true },

            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/products/CreateOrEdit/' + ${row.id}, 'تعديل المنتج')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/products/Delete/' + ${row.id}, 'حذف المنتج')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}
auctionDatatable = () => {
    $("#auction-datatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        paging: true,
        destroy: true,
        lengthChange: true,
        searching: true,
        ordering: true,
        lengthMenu: [7, 10, 25, 50, 75, 100],
        responsive: false,
        order: [[0, 'desc']],
        dom:
            '<"row"<"col-sm-12"<"col-sm-12"B>>>' + '<"row"<"col-sm-12 col-md-6"l>' + '<"col-sm-12 col-md-6"f>>' +
            '<"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        buttons: [
            {
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة مزاد</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Admin/Auctions/CreateOrEdit', 'إضافة المزاد');
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
            "url": "/Admin/Auctions/GetAuctionsData",
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
            { "data": "product.nameAr", "name": "Product Name", "autoWidth": true, "orderable": false },
            { "data": "startDate", "name": "StartDate", "autoWidth": true },
            { "data": "endDate", "name": "EndDate", "autoWidth": true },
            { "data": "startPrice", "name": "StartPrice", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/Auctions/ShowParticipants/' + ${row.id}, 'المشاركين')" class="btn btn-secondary btn-sm">المشاركين</button>` +
                            `<span>&nbsp;</span>` +
                            `<button onClick="showInPopup('/Admin/Auctions/CreateOrEdit/' + ${row.id}, 'تعديل المزاد')" class="btn btn-primary btn-sm">تعديل</button>` +
                            `<span>&nbsp;</span>` +
                            `<button onClick="showInPopup('/Admin/Auctions/Delete/' + ${row.id}, 'حذف المزاد')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}
purchaseDatatable = () => {
    $("#purchase-datatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        paging: true,
        destroy: true,
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
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة طلب</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Admin/Purchase/CreateOrEdit', 'إضافة طلب');
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
            "url": "/Admin/Purchase/GetPurchaseData",
            "type": "POST",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": true,
            "searchable": true
        },

        {
            "targets": [3],
            "searchable": false,
            render: function (data, type, row) {
                switch (data) {
                    case true: return 'تم تسديد كامل المبلغ'; break;
                    default: return 'لم يتم تسديد كامل المبلغ'; break;
                }
            }
        },],
        columns: [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "amount", "name": "amount", "autoWidth": true },
            { "data": "extraAmount", "name": "extraAmount", "autoWidth": true },
            { "data": "status", "name": "status", "autoWidth": true },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "phone", "name": "phone", "autoWidth": true },
            { "data": "detials", "name": "detials", "autoWidth": true },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            { "data": "user.name", "name": "name", "autoWidth": true, "orderable": false },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/Purchase/ShowProducts/' + ${row.id}, 'تفاصيل الطلب')" class="btn btn-primary btn-sm">عرض تفاصيل الطلب</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/Purchase/CreateOrEdit/' + ${row.id}, 'تعديل الطلب')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/Purchase/Delete/' + ${row.id}, 'حذف الطلب')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}

AssociationpurchaseDatatable = () => {
    $("#Associationpurchase-datatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        paging: true,
        destroy: true,
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
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة طلب</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Association/Purchase/CreateOrEdit', 'إضافة طلب');
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
            "url": "/Association/Purchase/GetPurchaseData",
            "type": "POST",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": true,
            "searchable": true
        },

        {
            "targets": [3],
            "searchable": false,
            render: function (data, type, row) {
                switch (data) {
                    case true: return 'تم تسديد كامل المبلغ'; break;
                    default: return 'لم يتم تسديد كامل المبلغ'; break;
                }
            }
        },],
        columns: [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "amount", "name": "amount", "autoWidth": true },
            { "data": "extraAmount", "name": "extraAmount", "autoWidth": true },
            { "data": "status", "name": "status", "autoWidth": true },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "phone", "name": "phone", "autoWidth": true },
            { "data": "detials", "name": "detials", "autoWidth": true },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            { "data": "user.name", "name": "name", "autoWidth": true, "orderable": false },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Association/Purchase/ShowProducts/' + ${row.id}, 'تفاصيل الطلب')" class="btn btn-primary btn-sm">عرض تفاصيل الطلب</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Association/Purchase/CreateOrEdit/' + ${row.id}, 'تعديل الطلب')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Association/Purchase/Delete/' + ${row.id}, 'حذف الطلب')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}
farmerDatatable = () => {
    $("#farmerDatatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        paging: true,
        destroy: true,
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
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة مزارع</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Admin/Farmer/CreateOrEdit', 'إضافة مزارع');
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
                    }
                ]
            }
        ],
        ajax: {
            "url": "/Admin/Farmer/GetFarmerData",
            "type": "POST",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": true,
            "searchable": true
        },

        ],
        columns: [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "phoneNumber", "name": "phonenumber", "autoWidth": true },
            { "data": "directorate.governorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "directorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/Farmer/ShowProducts/' + ${row.id}, 'تفاصيل المزارع')" class="btn btn-primary btn-sm">عرض تفاصيل المزارع</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/Farmer/CreateOrEdit/' + ${row.id}, 'تعديل المزارع')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Admin/Farmer/Delete/' + ${row.id}, 'حذف المزارع')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}

AssociationfarmerDatatable = () => {
    $("#AssociationfarmerDatatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        paging: true,
        destroy: true,
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
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة مزارع</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Association/Farmer/CreateOrEdit', 'إضافة مزارع');
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
                    }
                ]
            }
        ],
        ajax: {
            "url": "/Association/Farmer/GetFarmerData",
            "type": "POST",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": true,
            "searchable": true
        },

        ],
        columns: [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "phoneNumber", "name": "phonenumber", "autoWidth": true },
            { "data": "directorate.governorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "directorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Association/Farmer/ShowProducts/' + ${row.id}, 'تفاصيل المزارع')" class="btn btn-primary btn-sm">عرض تفاصيل المزارع</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Association/Farmer/CreateOrEdit/' + ${row.id}, 'تعديل المزارع')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Association/Farmer/Delete/' + ${row.id}, 'حذف المزارع')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}


AssociationauctionDatatable = () => {
    $("#Associationauction-datatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        paging: true,
        destroy: true,
        lengthChange: true,
        searching: true,
        ordering: true,
        lengthMenu: [7, 10, 25, 50, 75, 100],
        responsive: false,
        order: [[0, 'desc']],
        dom:
            '<"row"<"col-sm-12"<"col-sm-12"B>>>' + '<"row"<"col-sm-12 col-md-6"l>' + '<"col-sm-12 col-md-6"f>>' +
            '<"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        buttons: [
            {
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة مزاد</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Association/Auctions/CreateOrEdit', 'إضافة المزاد');
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
            "url": "/Association/Auctions/GetAuctionsData",
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
            { "data": "product.nameAr", "name": "Product Name", "autoWidth": true, "orderable": false },
            { "data": "startDate", "name": "StartDate", "autoWidth": true },
            { "data": "endDate", "name": "EndDate", "autoWidth": true },
            { "data": "startPrice", "name": "StartPrice", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Association/Auctions/ShowParticipants/' + ${row.id}, 'المشاركين')" class="btn btn-secondary btn-sm">المشاركين</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Association/Auctions/CreateOrEdit/' + ${row.id}, 'تعديل المزاد')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Association/Auctions/Delete/' + ${row.id}, 'حذف المزاد')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}



AssociationproductDatatable = () => {
    $("#AssociationproductDatatable").DataTable({
        processing: true,
        serverSide: true,
        destroy: true,
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
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة منتج</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Association/Products/CreateOrEdit', 'تعديل المنتج');
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
            "url": "/Association/Products/GetProductData",
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
            { "data": "nameAr", "name": "nameAr", "autoWidth": true },
            { "data": "category.name", "name": "Name", "autoWidth": true },
            { "data": "directorate.governorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "directorate.name", "name": "directorate", "autoWidth": true, "orderable": false },
            { "data": "quantity", "name": "Name", "autoWidth": true },
            { "data": "unit", "name": "Name", "autoWidth": true },
            { "data": "price", "name": "Name", "autoWidth": true },
            { "data": "status", "name": "Name", "autoWidth": true },

            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Association/products/CreateOrEdit/' + ${row.id}, 'تعديل المنتج')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Association/products/Delete/' + ${row.id}, 'حذف المنتج')" class="btn btn-danger btn-sm">حذف</button>`;
                },
                "name": "action",
                "autoWidth": true,
                "searchable": false,
                "orderable": false
            },
        ]
    });
}

AssociationcategoryDatatable = () => {
    $("#Associationcategory-datatable").DataTable({
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
                text: '<i class="bx bx-plus me-sm-2"></i><span class="d-none d-sm-inline-block">إضافة تصنيف</span>',
                className: 'dt-button create-new btn btn-primary m-2',
                action: function (e, dt, node, config) {
                    showInPopup('/Association/Category/CreateOrEdit', 'تعديل تصنيف');
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
            "url": "/Association/Category/GetCategoryData",
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
                    return `<button onClick="showInPopup('/Association/Category/CreateOrEdit/' + ${row.id}, 'تعديل الصنف')" class="btn btn-primary btn-sm">تعديل</button>` +
                        `<span>&nbsp;</span>` +
                        `<button onClick="showInPopup('/Association/Category/Delete/' + ${row.id}, 'حذف الصنف')" class="btn btn-danger btn-sm">حذف</button>`;
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
                        else if (res.type == "product") {
                            productDatatable();
                            AssociationproductDatatable();
                        }
                        else if (res.type == "category") {
                            categoryDatatable();
                            AssociationcategoryDatatable();
                        }
                        else if (res.type == "auctions") {
                            auctionDatatable();
                            AssociationauctionDatatable();
                        }
                        else if (res.type == "purchase") {
                            purchaseDatatable();
                            AssociationpurchaseDatatable();
                        }

                        else if (res.type == "farmer") {
                            farmerDatatable();
                            AssociationfarmerDatatable();
                        }
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
                        else if (res.type == "product") {
                            productDatatable();
                            AssociationproductDatatable();
                        }
                        else if (res.type == "category") {
                            categoryDatatable();
                            AssociationcategoryDatatable();
                        }
                        else if (res.type == "auctions") {
                            auctionDatatable();
                            AssociationauctionDatatable();
                        }
                        else if (res.type == "purchase") {
                            purchaseDatatable();
                            AssociationpurchaseDatatable();
                        }

                        else if (res.type == "farmer") {
                            farmerDatatable();
                            AssociationfarmerDatatable();
                        }
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