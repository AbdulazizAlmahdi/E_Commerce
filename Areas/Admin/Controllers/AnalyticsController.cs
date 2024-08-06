using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            ViewBag.Notificatins= _unitOfWork.GetRepository<Notification>().Include(u=>u.user).Where(a => a.IsRead == false).ToList();
            var userId = HttpContext.Session.GetString("_UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }


            var vm = new AnaylticsViewModel
            {
                Users = _unitOfWork.GetRepository<User>().GetAll(),
                Products = _unitOfWork.GetRepository<Product>().GetAll(),
                AuctionCount = _unitOfWork.GetRepository<Auction>().Count(),
                PurchaseCount = _unitOfWork.GetRepository<Purchase>().Count()
            };
            return View(vm);
        }
    }
}
