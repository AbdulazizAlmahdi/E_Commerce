//====================== Evaluationproduct (star rating) =============
$(document).ready(function () {
    $("#st1").click(function () {
        $(".Evaluationproduct .fa-star").css("color", "#BCBCBC");
        $("#st1").css("color", "#FFE622 ");
        $(".Evaluationproduct li").val('1');


    });
    $("#st2").click(function () {
        $(".Evaluationproduct .fa-star").css("color", "#BCBCBC");
        $("#st1, #st2").css("color", "#FFE622 ");
        $(".Evaluationproduct li").val('2');


    });
    $("#st3").click(function () {
        $(".Evaluationproduct .fa-star").css("color", "#BCBCBC")
        $("#st1, #st2, #st3").css("color", "#FFE622 ");
        $(".Evaluationproduct li").val('3');

    });
    $("#st4").click(function () {
        $(".Evaluationproduct .fa-star").css("color", "#BCBCBC");
        $("#st1, #st2, #st3, #st4").css("color", "#FFE622 ");
        $(".Evaluationproduct li").val('4');

    });
    $("#st5").click(function () {
        $(".Evaluationproduct .fa-star").css("color", "#BCBCBC");
        $("#st1, #st2, #st3, #st4, #st5").css("color", "#FFE622 ");
        $(".Evaluationproduct li").val('5');


    });

});
//====================== Evaluationaution (star rating) =============
$(document).ready(function () {
    $("#st1").click(function () {
        $(".Evaluationaution .fa-star").css("color", "#BCBCBC");
        $("#st1").css("color", "#FFE622 ");
        $(".Evaluationaution li").val('1');


    });
    $("#st2").click(function () {
        $(".Evaluationaution .fa-star").css("color", "#BCBCBC");
        $("#st1, #st2").css("color", "#FFE622 ");
        $(".Evaluationaution li").val('2');


    });
    $("#st3").click(function () {
        $(".Evaluationaution .fa-star").css("color", "#BCBCBC")
        $("#st1, #st2, #st3").css("color", "#FFE622 ");
        $(".Evaluationaution li").val('3');

    });
    $("#st4").click(function () {
        $(".Evaluationaution .fa-star").css("color", "#BCBCBC");
        $("#st1, #st2, #st3, #st4").css("color", "#FFE622 ");
        $(".Evaluationaution li").val('4');

    });
    $("#st5").click(function () {
        $(".Evaluationaution .fa-star").css("color", "#BCBCBC");
        $("#st1, #st2, #st3, #st4, #st5").css("color", "#FFE622 ");
        $(".Evaluationaution li").val('5');


    });

});


var btnCopy = $("#copybtn"),
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