using E_commerce.Models;
using E_commerce.Models.Repositories;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnalyticsController : Controller
    {

        private IAnalyticsRepository _analyticsRepository;
        public readonly WebContext _context;

        public AnalyticsController(IAnalyticsRepository analyticsRepository,   WebContext context)
        {
            _analyticsRepository = analyticsRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            //var userId= HttpContext.Session.GetString("_UserId");

            //if (userId==null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
  
            var vm = new AnaylticsViewModel
            {
                //Users = _analyticsRepository.GetUsers()
                //Product = (Models.Product)_analyticsRepository.GetProducts()
            };
            return View(vm);
        }
    }
}
