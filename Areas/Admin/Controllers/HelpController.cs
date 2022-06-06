using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HelpController : Controller{


        private IHelpRepository repository;

        public HelpController(IHelpRepository iHelpRepository)
        {  
            repository = iHelpRepository;
        }

        public IActionResult index(){
            var helps = repository.GetAllHelpRequests();
            return View(helps);
        }

        public IActionResult Delete(int id){
            var help = repository.Find(id);
            repository.Delete(help);
            return RedirectToAction("index");
        }

        public IActionResult Details(int id){
            var help = repository.Find(id);
            return View(help);
        }

        

    }
}