//-----------------for shoping cart----------------

 $(document).ready(function(){
    update_amounts();
    $('.qty, .price').on('keyup keypress blur change',
        function(e){
            update_amounts();
        });
    
});
var qty=$(this).find('.qty').val

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
//-----------------for quantity-increment-ro0decrement cart----------------
var uncrementQty;
var ecrementQty;
var plusBtn= $(".cartQtyPlus");
var minusBtn= $(".cartQtyMinus");
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
