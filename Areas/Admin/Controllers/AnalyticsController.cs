using E_commerce.Models.Repositories;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnalyticsController : Controller
    {

        private IAnalyticsRepository _analyticsRepository;

        public AnalyticsController(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public IActionResult Index()
        {
            var vm = new AnaylticsViewModel
            {
                Users = _analyticsRepository.GetUsers()
            };
            return View(vm);
        }
    }
}
