//btnStop btnSold btnActive btnPurchased btnOrder
//tableStop tableSold tableActive tablePurchased tableOrder 
//$(document).ready(function(){ 
//   $('#tableActive').DataTabe();
// });
$(document).ready(function () {
    $("body").on("change", "#imageprofile", function (e) {
        previewImage(this, 'previewprofile');
    });


});
function previewImage(input, targetDivToShow) {
    var files = input.files;
    var filename;
    var done = function (url) {
        filename = $('#' + targetDivToShow).attr('src', url);
        $('#' + targetDivToShow).attr('src', url);

    };
    if (input.files && input.files[0]) {
        file = input.files[0];
        if (URL) {

            done(URL.createObjectURL(file));
        }
        else if (FileReader) {
            var reader = new FileReader();
            reader.onload = function (e) {

                done(reader.result);

            }
            reader.readAsDataURL(file);


        }

    }


}




var btnStop = $("#btnStop"),
    tableStop = $("#tableStop"),

    btnSold = $("#btnSold"),
    tableSold = $("#tableSold"),

    btnActive = $("#btnActive"),
    tableActive = $("#tableActive"),

    btnPurchased = $("#btnPurchased"),
    tablePurchased = $("#tablePurchased"),

    btnOrder = $("#btnOrder"),
    tableOrder = $("#tableOrder");

//========================= initialize style view =====================

tableActive.show().css('display', 'table').DataTable();
$('div#tableActive_wrapper').addClass('table-responsive');


btnStop.click(function () {


    if (tableStop.css('display', 'table') == true) {
        /*
        tableStop.style.display="table";
        
        tableSold.style.display="none";
        tableActive.style.display="none";
        tablePurchased.style.display="none";
        tableOrder.style.display="none";
        $( 'div#tableStop_wrapper').show();
        
        */

        tableSold.hide();
        tableActive.hide();
        tablePurchased.hide();
        tableOrder.hide();
        $('div#tableStop_wrapper').show();







    }
    else {
        $('div#tableStop_wrapper').show();
        $('div#tableSold_wrapper').hide();
        $('div#tableActive_wrapper').hide();
        $('div#tablePurchased_wrapper').hide();
        $('div#tableOrder_wrapper').hide();

        $('.tableStop').DataTable();
        $('div#tableStop_wrapper').addClass('table-responsive');

        tableSold.hide();
        tableActive.hide();
        tablePurchased.hide();
        tableOrder.hide();
    }
});
btnSold.click(function () {
    if (tableSold.css('display', 'table') == true) {

        tableStop.hide();
        tableActive.hide();
        tablePurchased.hide();
        tableOrder.hide();
        $('div#tableSold_wrapper').show();

        /* tableStop.style.display="none";
         tableSold.style.display="table";
         tableActive.style.display="none";
         tablePurchased.style.display="none";
         tableOrder.style.display="none";
         $( 'div#tableSold_wrapper').show();
        */

    }
    else {
        tableStop.hide();
        $('div#tableStop_wrapper').hide();
        $('div#tableSold_wrapper').show();
        $('div#tableActive_wrapper').hide();
        $('div#tablePurchased_wrapper').hide();
        $('div#tableOrder_wrapper').hide();
        //tableSold.style.display="table";
        $('.tableSold').DataTable();
        $('div#tableSold_wrapper').addClass('table-responsive');

        tableActive.hide();
        tablePurchased.hide();
        tableOrder.hide();
    }
});
btnActive.click(function () {
    if (tableActive.css('display', 'table') == true) {

        /* tableStop.style.display="none";
         tableSold.style.display="none";
         tableActive.style.display="table";
         tablePurchased.style.display="none";
         tableOrder.style.display="none";
         $( 'div#tableActive_wrapper').show();
         */
        tableStop.hide();
        tableSold.hide();
        tablePurchased.hide();
        tableOrder.hide();
        $('div#tableActive_wrapper').show();



    }
    else {

        tableStop.hide();
        tableSold.hide();
        $('div#tableStop_wrapper').hide();
        $('div#tableSold_wrapper').hide();
        $('div#tableActive_wrapper').show();
        $('div#tablePurchased_wrapper').hide();
        $('div#tableOrder_wrapper').hide();
        //tableActive.style.display="table";
        $('.tableActive').DataTable();
        $('div#tableActive_wrapper').addClass('table-responsive');
        tablePurchased.hide();
        tableOrder.hide();
    }
});
btnPurchased.click(function () {
    if (tablePurchased.css('display', 'table') == true) {

        tableStop.hide();
        tableSold.hide();
        tableActive.hide();
        //tablePurchased.style.display="table";
        tableOrder.hide();
        $('div#tablePurchased_wrapper').show();

    }
    else {

        tableStop.hide();
        tableSold.hide();
        tableActive.hide();
        $('div#tableStop_wrapper').hide();
        $('div#tableSold_wrapper').hide();
        $('div#tableActive_wrapper').hide();
        $('div#tablePurchased_wrapper').show();

        $('div#tableOrder_wrapper').hide();
        //tablePurchased.style.display="table";
        $('.tablePurchased').DataTable();
        $('div#tablePurchased_wrapper').addClass('table-responsive');
        tableOrder.hide();
    }
});
btnOrder.click(function () {
    if (tableOrder.css('display', 'table') == true) {

        tableStop.hide();
        tableSold.hide();
        tableActive.hide();
        tablePurchased.hide();
        // tableOrder.style.display="table";
        $('div#tableOrder_wrapper').show();


    }
    else {
        tableStop.hide();
        tableSold.hide();
        tableActive.hide();
        tablePurchased.hide();
        $('div#tableStop_wrapper').hide();
        $('div#tableSold_wrapper').hide();
        $('div#tableActive_wrapper').hide();
        $('div#tablePurchased_wrapper').hide();
        $('div#tableOrder_wrapper').show();
        //tableOrder.style.display="table";
        $('.tableOrder').DataTable();
        $('div#tableOrder_wrapper').addClass('table-responsive');
    }
});