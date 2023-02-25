using System.Net;
using EasMe.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceTrackingAutomation.App.Filters;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Helpers;
using ServiceTrackingAutomation.Domain.Models;

namespace ServiceTrackingAutomation.App.Controllers
{
    [AuthFilter]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        private static readonly IEasLog logger = EasLogFactory.CreateLogger();

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult AccountDetails()
        {
            var user = HttpContext.GetUser();
            return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = HttpContext.GetUser();
            var res = _userManager.ChangePassword(user.Id,model);
            if (res.IsFailure)
            {
                ModelState.AddModelError("",res.ErrorCode);
                return View(model);
            }
            return RedirectToAction("Logout","Home");
        }
    }

}
