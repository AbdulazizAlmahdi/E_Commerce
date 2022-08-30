function getPageList(totalPages, page ,maxLength){
    function rang(start , end){
        return Array.from(Array(end - start + 1),(_,i) => i + start);
    }

    var sideWidth = maxLength < 9 ? 1 : 2;
    var leftWidth = (maxLength - sideWidth * 2 - 3) >> 1;  
    var rightWidth = (maxLength - sideWidth * 2 - 3) >> 1;

    if(totalPages <= maxLength ){
        return rang(1 , totalPages);
    }

    if(page <= maxLength - sideWidth - 1 - rightWidth){
        return rang(1 , maxLength - sideWidth - 1).concat(0 , rang(totalPages - sideWidth + 1 ,totalPages));
    }

    if(page >= totalPages - sideWidth - 1 - rightWidth){
        return rang(1 , sideWidth).concat(0 , rang(totalPages - sideWidth - 1 - rightWidth - leftWidth ,totalPages));
    }
    return rang(1 , sideWidth).concat(0, rang(page - leftWidth ,page + rightWidth), 0 ,rang(totalPages - sideWidth + 1, totalPages));
}

 $(function(){

    var numberOfItem = $(" .mypaginationindex .card ").length;
    var limitPerPage=6; //how many card items visible per a page
    var totalPage = Math.ceil(numberOfItem/limitPerPage);
    var paginationSize = 5 ; //how many page elements visible in the pagination
    var currentPage ;


    function showPage(whichPage){

        if(whichPage < 1 || whichPage > totalPage) return false;
        currentPage = whichPage;

        $(" .mypaginationindex .card ").hide().slice(( currentPage - 1)*limitPerPage , currentPage* limitPerPage).show();
        $(".pagination li").slice(1 , -1).remove();

        getPageList(totalPage , currentPage , paginationSize).forEach(item => {
            $("<li>").addClass("page-item").addClass(item ? "current-page" : "dots")
            .toggleClass("active", item === currentPage).append($("<a>").addClass("page-link")
            .attr({href: "javascript:void(0)"}).text(item ||"...")).insertBefore(".next-page");
        });

        $(".previous-page").toggleClass("disable", currentPage === 1);
        $(".next-page").toggleClass("disable", currentPage === totalPage);

        return true
    }
    $(".pagination").append(
        $("<li>").addClass("page-item").addClass("previous-page").append($("<a>").addClass("page-link").attr({href: "javascript:void(0)"}).text("prev")),
        $("<li>").addClass("page-item").addClass("next-page").append($("<a>").addClass("page-link").attr({href: "javascript:void(0)"}).text("next")),

        );
    $(".mypaginationindex").show();
    
    showPage(1);

    $(document).on("click",".pagination li.current-page:not(.active)",function(){
        return showPage(+$(this).text())
    });
    $(".next-page").on("click",function(){
        return showPage(currentPage + 1);
    });
    $(".previous-page").on("click",function(){
        return showPage(currentPage - 1);
    });

 });




















