function scroll_l(){
    var left =document.querySelector(".scrollitem");
    left.scrollBy(50,0)
  }
  function scroll_r(){
    var right =document.querySelector(".scrollitem");
    right.scrollBy(-50,0)
  }

var toastTrigger = document.getElementById('liveToastBtn')
var toastLiveExample = document.getElementById('liveToast')
if (toastTrigger) {
  toastTrigger.addEventListener('click', function () {
    var toast = new bootstrap.Toast(toastLiveExample)

    toast.show()
  })
}