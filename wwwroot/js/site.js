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