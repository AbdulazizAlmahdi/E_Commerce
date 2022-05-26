using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce.Models.ViewModels;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
       private IUsersRepository<User> users;


        public UsersController(IUsersRepository<User> usersRepository) => this.users = usersRepository;
        public IActionResult Index(int page=1)
        {
            const int pageSize = 10;
            if (page < 1)
                page = 1;
            int Count = users.Users.Count();
            var pagingInfo = new PagingInfo(Count, page, pageSize);
          int  recSkip = (page - 1) * pageSize;
            var data = users.Users.Skip(recSkip).Take(pagingInfo.ItemsPerPage).ToList();
            this.ViewBag.PagingInfo = pagingInfo;
            return View(data);
        }
        //public IActionResult Index(int pageNumber=1)
        //{

        //    var user = users.Users.OrderBy(u => u.Id)
        //        .Skip((pageNumber - 1) * PageSize)
        //        .Take(PageSize);

        //    var vm = new UserFilterViewModel
        //    {
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = pageNumber,
        //            ItemsPerPage = PageSize,
        //            TotalItems = users.Users.Count()
        //        },
        //        Users = user
        //    };

        //    return View(vm);
        //}
    }
}
