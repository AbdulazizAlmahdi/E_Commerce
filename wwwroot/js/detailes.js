var btnCopy =$("#copybtn"),
    numberorder =$("#numberorder"),
    copybtnmessage =$("#copybtnmessage");
  

   /* const numberorder =document.getElementById("numberorder");
    const btnCopy =document.getElementById("copybtn");
    btnCopy.onclick=function(){

        myinput.select();
        document.execCommand("copy");
        


    } */
    
    //================copy text to clipboard===============================
   
   function copyToClipboard (element){
       var $temp=$("<input>");
       $("body").append($temp);
       $temp.val($(element).text()).select();
       document.execCommand("copy");
       $temp.remove();
   }


//======================================================================

    

    btnCopy.click(function(){
        if(copybtnmessage.css('display')=='block')
        {
            copybtnmessage.css('display','none');
        }
        else
        {
            copybtnmessage.css('display','block');
            copyToClipboard(numberorder);
            
            
        }

    });