using EasMe.Logging;
using EasMe.Result;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;
using System.Net;

namespace ServiceTrackingAutomation.App.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerManager _customerManager;
        private static readonly IEasLog logger = EasLogFactory.CreateLogger();

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }
        [HttpGet]
        public IActionResult List()
        {
            var res = _customerManager.GetCustomers();
            return View(res);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            var res = _customerManager.AddCustomer(customer);
            if (res.IsFailure)
            {
                ModelState.AddModelError("",res.ErrorCode);
                return View(customer);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var res = _customerManager.GetCustomer(id);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View();
            }
            return View(res.Data);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            var res = _customerManager.UpdateCustomer(customer);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(customer);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var res = _customerManager.DisableCustomer(id);
            if (res.IsFailure) return StatusCode((int)HttpStatusCode.InternalServerError);
            return RedirectToAction("List");

        }

        [HttpPost]
        public IActionResult Enable(int id)
        {
            var res = _customerManager.EnableCustomer(id);
            if (res.IsFailure) return StatusCode((int)HttpStatusCode.InternalServerError);
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult InvalidCustomers()
        {
            var res = _customerManager.GetInvalidCustomers();
            return View(res);
        }



    }
}
