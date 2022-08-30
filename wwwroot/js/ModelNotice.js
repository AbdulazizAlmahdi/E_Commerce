var btnNotice = $("#cuccessNotice"),
    divNotic = $("#cuccessDiv"),
    btnClose = $("#btnClose"),

    btnCloceaution = $("#btnCloseaution"),
    btnNoticeaution = $("#cuccessNoticeaution"),
    divNoticaution = $("autioncuccessDiv"),

    btnClocebasket = $("#btnClosebasket"),
    btnNoticebasket = $("#basketcuccessNotice"),
    divNoticbasket = $("#baskecuccessDiv");

btnNotice.click(function()
    {
        if(divNotic.css('display')=='block')
        {
            divNotic.css('display','none');
        }
        else
        {
            divNotic.css('display','block');
        }
    } );
 btnClose.click(function()
    {
        if(divNotic.css('display')=='block')
        {
            divNotic.css('display','none');
        }
        else
        {
            divNotic.css('display','block');
        }
 });


btnNoticeaution.click(function () {
    if (divNoticaution.css('display') == 'block') {
        divNoticaution.css('display', 'none');
    }
    else {
        divNoticaution.css('display', 'block');
    }
});
btnCloceaution.click(function () {
    if (divNoticaution.css('display') == 'block') {
        divNoticaution.css('display', 'none');
    }
    else {
        divNoticaution.css('display', 'block');
    }
});



btnNoticebasket.click(function () {
    if (divNoticbasket.css('display') == 'block') {
        divNoticbasket.css('display', 'none');
    }
    else {
        divNoticbasket.css('display', 'block');
    }
});
btnClocebasket.click(function () {
    if (divNoticbasket.css('display') == 'block') {
        divNoticbasket.css('display', 'none');
    }
    else {
        divNoticbasket.css('display', 'block');
    }
});






