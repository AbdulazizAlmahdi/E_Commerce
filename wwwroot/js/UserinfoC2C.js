//btnStop btnSold btnActive btnPurchased btnOrder
//tableStop tableSold tableActive tablePurchased tableOrder 
//$(document).ready(function(){ 
 //   $('#tableActive').DataTabe();
 // });


var btnStop =$("#btnStop"),
    tableStop=$("#tableStop"),

    btnSold =$("#btnSold"),
    tableSold=$("#tableSold"),

    btnActive =$("#btnActive"),
    tableActive=$("#tableActive"),

    btnPurchased =$("#btnPurchased"),
    tablePurchased=$("#tablePurchased"),

    btnOrder =$("#btnOrder"),
    tableOrder=$("#tableOrder") ;

    tableActive.show().css('display','table').DataTable();

    btnStop.click(function()
    {
        

        if(tableStop.css('display','table')==true)
        {
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
            $( 'div#tableStop_wrapper').show();






        }
        else
        {
            $( 'div#tableStop_wrapper').show();
            $( 'div#tableSold_wrapper').hide();
            $( 'div#tableActive_wrapper').hide();
            $( 'div#tablePurchased_wrapper').hide();
            $( 'div#tableOrder_wrapper').hide();
           
            $('.tableStop').DataTable();

            tableSold.hide();
            tableActive.hide();
            tablePurchased.hide();
            tableOrder.hide();
        }
    } );
    btnSold.click(function()
    {
        if(tableSold.css('display','table')==true)
        {
            
            tableStop.hide();
            tableActive.hide();
            tablePurchased.hide();
            tableOrder.hide();
            $( 'div#tableStop_wrapper').show();

           /* tableStop.style.display="none";
            tableSold.style.display="table";
            tableActive.style.display="none";
            tablePurchased.style.display="none";
            tableOrder.style.display="none";
            $( 'div#tableSold_wrapper').show();
           */
            
        }
        else
        {
            tableStop.hide();
            $( 'div#tableStop_wrapper').hide();
             $( 'div#tableSold_wrapper').show();
             $( 'div#tableActive_wrapper').hide();
             $( 'div#tablePurchased_wrapper').hide();
             $( 'div#tableOrder_wrapper').hide();
            //tableSold.style.display="table";
            $('.tableSold').DataTable();
            tableActive.hide();
            tablePurchased.hide();
            tableOrder.hide();
        }
    } );
    btnActive.click(function()
    {
        if(tableActive.css('display','table')==true)
        {
            
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
            $( 'div#tableStop_wrapper').show();
            
            
            
        }
        else
        {
            
            tableStop.hide();
            tableSold.hide();
            $( 'div#tableStop_wrapper').hide();
             $( 'div#tableSold_wrapper').hide();
             $( 'div#tableActive_wrapper').show();
             $( 'div#tablePurchased_wrapper').hide();
             $( 'div#tableOrder_wrapper').hide();
           //tableActive.style.display="table";
            $('.tableActive').DataTable();
            tablePurchased.hide();
            tableOrder.hide();
        }
    } );
    btnPurchased.click(function()
    {
        if(tablePurchased.css('display','table')==true)
        {
            
            tableStop.hide();
            tableSold.hide();
            tableActive.hide();
            //tablePurchased.style.display="table";
            tableOrder.hide();
            $( 'div#tablePurchased_wrapper').show();
            
        }
        else
        {
        
        tableStop.hide();
        tableSold.hide();
        tableActive.hide();
        $( 'div#tableStop_wrapper').hide();
        $( 'div#tableSold_wrapper').hide();
        $( 'div#tableActive_wrapper').hide();
        $( 'div#tablePurchased_wrapper').show();
        $( 'div#tableOrder_wrapper').hide();
       //tablePurchased.style.display="table";
        $('.tablePurchased').DataTable();
        tableOrder.hide();
        }
    } );
    btnOrder.click(function()
    {
        if(tableOrder.css('display','table')==true)
        {
            
            tableStop.hide();
            tableSold.hide();
            tableActive.hide();
            tablePurchased.hide();
           // tableOrder.style.display="table";
            $( 'div#tableOrder_wrapper').show();
            
            
        }
        else
        {
            tableStop.hide();
         tableSold.hide();
         tableActive.hide();
         tablePurchased.hide();
         $( 'div#tableStop_wrapper').hide();
         $( 'div#tableSold_wrapper').hide();
         $( 'div#tableActive_wrapper').hide();
         $( 'div#tablePurchased_wrapper').hide();
         $( 'div#tableOrder_wrapper').show();
         //tableOrder.style.display="table";
         $('.tableOrder').DataTable();
        }
    });
