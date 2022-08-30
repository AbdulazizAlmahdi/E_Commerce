$(document).ready(function () {
    var basketTable = $('#basketTable');
    //basketTable.show().css('display', 'table').DataTable()
    //$('div#basketTable_wrapper').show();
    basketTable.DataTable();



});
//-----------------for shoping cart----------------

 $(document).ready(function(){
    update_amounts();
    $('.qty, .price').on('keyup keypress blur change',
        function(e){
            update_amounts();
        });
   
    
});
var qty = $(this).find('.qty').val();

function update_amounts() {
    var sum=0.0;
    $('#basketTable > tbody >tr').each(function(){
        var qty=$(this).find('.qty').val();
        var price=$(this).find('.price').val();
        var amount=(qty*price);
            sum+=amount;

        $(this).find('.amount').text(''+amount);

    }) ;
    $('.total').text(sum);

}
//-----------------for quantity-increment-decrement cart----------------

var plusBtn= $(".cartQtyPlus");
var minusBtn= $(".cartQtyMinus");
var delBtn=$(".btnDelete");
var incrementQty=plusBtn.click(function(){
    var $n=$(this)
    .parent(".button-container")
    .find(".qty");
    $n.val(Number($n.val())+1);
    update_amounts();
});
var decrementQty=minusBtn.click(function(){
    var $n=$(this)
    .parent(".button-container")
    .find(".qty");
    var QtyVal=Number($n.val());
    if (QtyVal>0){
        $n.val(QtyVal-1);
    }
    
    update_amounts();
});

//----------------------------------
var btndelete=delBtn.click(function(){
    var $n=$(this)
    .parent(".button-container")
    .find(".qty");
    var QtyVal=Number($n.val());
    if (QtyVal>0 ){
        $n.val(QtyVal*0);
    }
    
    update_amounts();
});

//--------------------remove product item from basket------------------
// $('#basketTable > tbody >tr')
var productTr=document.getElementById("productTr"),
    btnDelete=document.getElementById("btnDelete");

 
  /* btnDelete .onclick= function()
    {
        if(productTr.style.display=="table-row")
        {
            productTr.style.display="none";
        }
        else
        {
            productTr.style.display="table-row";
        }

    } 
*/

     function removeProduct(item) {
         document.getElementById(item).style.display = 'none';
         $('#' + item).remove();
    }
    $("#btnDelete1").on('click', function () {
        removeProduct('productTr1');
    }); 
    $("#btnDelete2").on('click', function () {
        removeProduct('productTr2');
    });





