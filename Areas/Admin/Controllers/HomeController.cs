using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {


        public IActionResult Index(int pageNum)
        {

            var userId = HttpContext.Session.GetString("_UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
