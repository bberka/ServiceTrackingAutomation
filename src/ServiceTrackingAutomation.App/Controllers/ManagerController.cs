using System.Net;
using EasMe.Logging;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackingAutomation.App.Filters;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.DTOs;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Domain.Enums;
using ServiceTrackingAutomation.Domain.Helpers;

namespace ServiceTrackingAutomation.App.Controllers
{
    [AuthFilter(RoleType.Owner)]
    public class ManagerController : Controller
    {
        private readonly IUserManager _userManager;
        private static readonly IEasLog logger = EasLogFactory.CreateLogger();

        public ManagerController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult List()
        {
            var userNo = HttpContext.GetUser().Id;
            return View(_userManager.GetUsers(userNo));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var userNo = HttpContext.GetUser().Id;
            if (userNo == id) return RedirectToAction("List");
            var result = _userManager.GetUser(id);
            if (result.IsFailure)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userNo = HttpContext.GetUser().Id;
            if (userNo == id) return RedirectToAction("List");
            var result = _userManager.GetUser(id);
            if (result.IsFailure)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            return View(result.Data);

        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var res = _userManager.UpdateUser(user);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(user);
            }
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var userNo = HttpContext.GetUser().Id;
            if (userNo == id) return RedirectToAction("List");
            var result = _userManager.DisableUser(id);
            if (result.IsFailure)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Enable(int id)
        {
            var userNo = HttpContext.GetUser().Id;
            if (userNo == id) return RedirectToAction("List");
            var result = _userManager.EnableUser(id);
            if (result.IsFailure)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            return RedirectToAction("List");

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var res = _userManager.CreateUser(user);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(user);
            }
            return RedirectToAction("List");
        }
    }
}
