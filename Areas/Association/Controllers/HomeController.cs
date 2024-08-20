using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Areas.Association.Controllers
{
    [Area("Association")]
    public class HomeController : Controller
    {


        public IActionResult Index(int pageNum)
        {
            var userId = HttpContext.Session.GetString("_AssUserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
