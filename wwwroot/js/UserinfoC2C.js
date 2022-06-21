//btnStop btnSold btnActive btnPurchased btnOrder
//tableStop tableSold tableActive tablePurchased tableOrder 

var btnStop =document.getElementById("btnStop"),
    tableStop=document.getElementById("tableStop"),

    btnSold =document.getElementById("btnSold"),
    tableSold=document.getElementById("tableSold"),

    btnActive =document.getElementById("btnActive"),
    tableActive=document.getElementById("tableActive"),

    btnPurchased =document.getElementById("btnPurchased"),
    tablePurchased=document.getElementById("tablePurchased"),

    btnOrder =document.getElementById("btnOrder"),
    tableOrder=document.getElementById("tableOrder") ;

    btnStop .onclick= function()
    {
        if(tableStop.style.display=="table")
        {
            tableStop.style.display="none";
            tableSold.style.display="none";
            tableActive.style.display="none";
            tablePurchased.style.display="none";
            tableOrder.style.display="none";
        }
        else
        {
            tableStop.style.display="table";
            tableSold.style.display="none";
            tableActive.style.display="none";
            tablePurchased.style.display="none";
            tableOrder.style.display="none";
        }
    } 
    btnSold .onclick= function()
    {
        if(tableSold.style.display=="table")
        {
            tableStop.style.display="none";
            tableSold.style.display="none";
            tableActive.style.display="none";
            tablePurchased.style.display="none";
            tableOrder.style.display="none";
        }
        else
        {
            tableStop.style.display="none";
            tableSold.style.display="table";
            tableActive.style.display="none";
            tablePurchased.style.display="none";
            tableOrder.style.display="none";
        }
    } 
    btnActive .onclick= function()
    {
        if(tableActive.style.display=="table")
        {
            tableStop.style.display="none";
            tableSold.style.display="none";
            tableActive.style.display="none";
            tablePurchased.style.display="none";
            tableOrder.style.display="none";
        }
        else
        {
            
            tableStop.style.display="none";
            tableSold.style.display="none";
            tableActive.style.display="table";
            tablePurchased.style.display="none";
            tableOrder.style.display="none";
        }
    } 
    btnPurchased .onclick= function()
    {
        if(tablePurchased.style.display=="table")
        {
            tableStop.style.display="none";
            tableSold.style.display="none";
            tableActive.style.display="none";
            tablePurchased.style.display="none";
            tableOrder.style.display="none";
        }
        else
        {
            tableStop.style.display="none";
            tableSold.style.display="none";
            tableActive.style.display="none";
            tablePurchased.style.display="table";
            tableOrder.style.display="none";
        }
    } 
    btnOrder .onclick= function()
    {
        if(tableOrder.style.display=="table")
        {
            tableStop.style.display="none";
            tableSold.style.display="none";
            tableActive.style.display="none";
            tablePurchased.style.display="none";
            tableOrder.style.display="none";
        }
        else
        {
            tableStop.style.display="none";
            tableSold.style.display="none";
            tableActive.style.display="none";
            tablePurchased.style.display="none";
            tableOrder.style.display="table";
        }
    } 