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

        public int PageSize = 6;

        public UsersController(IUsersRepository<User> usersRepository) => this.users = usersRepository;
        public IActionResult Index(int pageNumber=1)
        {

            var user = users.List().OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize);

            var vm = new UserFilterViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = PageSize,
                    TotalItems = users.List().Count()
                },
                Users = user
        };

            return View(vm);
        }
    }
}
