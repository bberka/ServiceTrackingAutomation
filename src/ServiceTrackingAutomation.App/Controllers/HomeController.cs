using Microsoft.AspNetCore.Mvc;
using ServiceTrackingAutomation.App.Models;
using System.Diagnostics;
using EasMe.Logging;
using ServiceTrackingAutomation.App.Filters;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Helpers;
using ServiceTrackingAutomation.Domain.Models;

namespace ServiceTrackingAutomation.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthManager _authManager;

        private static readonly IEasLog logger = EasLogFactory.CreateLogger(); 
        public HomeController(IAuthManager authManager)
        {
            _authManager = authManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.IsAuthenticated())
            {
                return RedirectToAction("Statistics");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (HttpContext.IsAuthenticated())
            {
                return RedirectToAction("Statistics");
            }
            var res = _authManager.Authenticate(model);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                logger.Warn("Login failed: " + model.EmailAddress, res.Rv + res.ErrorCode);
                return View(model);
            }
            HttpContext.SetUser(res.Data);
            logger.Info("Login success: " + model.EmailAddress, res.Rv + res.ErrorCode);
            return RedirectToAction("Statistics");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            var user = HttpContext.GetUser();
            logger.Info("Logging out: " + user.Id);
            HttpContext.RemoveAuth();
            return RedirectToAction("Index");
        }
        [AuthFilter]
        public IActionResult Statistics()
        {
            return View();

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}