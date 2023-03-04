using System.Net;
using EasMe.Logging;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackingAutomation.App.Filters;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.DTOs;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Domain.Enums;

namespace ServiceTrackingAutomation.App.Controllers
{
    [AuthFilter(RoleType.Owner, RoleType.Admin)]
    public class ServiceController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private static readonly IEasLog logger = EasLogFactory.CreateLogger();

        public ServiceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = _serviceManager.GetService(id);
            if (result.IsFailure) return StatusCode((int)HttpStatusCode.InternalServerError);
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(_serviceManager.GetServices());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                
                return View(service);
            }
            var res = _serviceManager.AddService(service);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(service);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _serviceManager.GetService(id);
            if (result.IsFailure) return RedirectToAction("List");
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Edit(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View(service);
            }
            var res = _serviceManager.UpdateService(service);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(service);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _serviceManager.DisableService(id);
            if (result.IsFailure) return StatusCode((int)HttpStatusCode.InternalServerError);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Enable(int id)
        {
            var result = _serviceManager.EnableService(id);
            if (result.IsFailure) return StatusCode((int)HttpStatusCode.InternalServerError);
            return RedirectToAction("List");
        }
      
       
    }
}
