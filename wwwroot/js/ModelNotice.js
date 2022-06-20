var btnNotice =document.getElementById("cuccessNotice"),
    divNotic=document.getElementById("cuccessDiv"),
    btnClose=document.getElementById("btnClose");

btnNotice .onclick= function()
    {
        if(divNotic.style.display=="block")
        {
            divNotic.style.display="none";
        }
        else
        {
            divNotic.style.display="block";
        }
    } 
    btnClose .onclick= function()
    {
        if(divNotic.style.display=="block")
        {
            divNotic.style.display="none";
        }
        else
        {
            divNotic.style.display="block";
        }
    } 
