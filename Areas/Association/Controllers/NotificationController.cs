using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static e_commerce.Helper;

namespace E_commerce.Areas.Association.Controllers
{
    [Area("Association")]

    public class NotificationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [NoDirectAccess]
        public IActionResult Index(int id)
        {
            Notification notification=_unitOfWork.GetRepository<Notification>().GetById(id);
            notification.IsRead = true;
            _unitOfWork.GetRepository<Notification>().Update(notification);
            _unitOfWork.SaveChanges();
            var url=notification.Url.Split('/');
            return RedirectToAction(url[1],url[0]);
        }

        
    }
}
