showInPopup=(url,title)=>{
    $.ajax({
        type: "GET",
        url: url,
        success: function(data){
        $("#form-modal .modal-body").html(data);
        $("#form-modal .modal-title").html(title);
        $("#form-modal").modal("show");
        }
    })
}
$(document).ready(function () {
    userDatatable();
});  
userDatatable = () => {
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
            "visible": true,
            "searchable": true
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "place.name", "name": "job name", "autoWidth": true, "orderable": false  },
            { "data": "phone.number", "name": "phone", "autoWidth": true, "orderable": false },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "createdAt", "name": "createdAt", "autoWidth": true },
            { "data": "userStatus.name", "name": "createdAt", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    return `<button onClick="showInPopup('/Admin/Users/CreateOrEdit/' + ${row.id}, 'تعديل المستخدم')" class="btn btn-primary btn-sm">تعديل</button>`+
                    `<span>&nbsp;</span>`+
                        `<button onClick="showInPopup('/Admin/Users/Delete/' + ${row.id}, 'حذف المستخدم')" class="btn btn-danger btn-sm">حذف</button>`;
                }
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
                console.log("dddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd " + res.isValid + res.html+" dddddddddddddddddddddd");
                if (res.status=="success") {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    userDatatable();
                    $('#successToast .successToastTitle').html("إضافة المستخدم");
                    $('#successToast .successToastBody').html("تمت إضافة المستخدم بنجاح");
                    new bootstrap.Toast(document.getElementById('successToast')).show();

                }
                else if (res.status=="validation-error") {
                    $('#form-modal .modal-body').html(res.html);
                }
                else {
                    console.log("Error");
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    userDatatable();
                    $('#errorToast .errorToastTitle').html("إضافة المستخدم");
                    $('#errorToast .errorToastBody').html("حدث خطأ أثناء إضافة المستخدم");
                    new bootstrap.Toast(document.getElementById('errorToast')).show();
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

jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

