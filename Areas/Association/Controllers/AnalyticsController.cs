using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace E_commerce.Areas.Association.Controllers
{
    [Area("Association")]
    public class AnalyticsController : Controller
    {

        /*        private IAnalyticsRepository _analyticsRepository;
                public readonly WebContext _context;*/
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsController(/*IAnalyticsRepository analyticsRepository,   WebContext context,*/IUnitOfWork unitOfWork)
        {
            // _analyticsRepository = analyticsRepository;
            // _context = context;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("_AssUserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Notificatins = _unitOfWork.GetRepository<Notification>().Include(u => u.user).Where(a => a.IsRead == false && a.user.UsersId == int.Parse(userId)).ToList();

            var vm = new AnaylticsViewModel
            {
                Products = _unitOfWork.GetRepository<Product>().Find(u => u.UserId == Convert.ToInt32(userId)),
                CatgoryCount = _unitOfWork.GetRepository<Category>().Find(u => u.UserId == Convert.ToInt32(userId)).Count()
            };
            return View(vm);
        }
    }
}
