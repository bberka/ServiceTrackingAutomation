using System.Net;
using EasMe.Logging;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackingAutomation.App.Filters;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.App.Controllers
{
    [AuthFilter]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        private static readonly IEasLog logger = EasLogFactory.CreateLogger();

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        public IActionResult List()
        {
            return View(_productManager.GetProducts());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = _productManager.GetProduct(id);
            if (result.IsFailure)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _productManager.GetProduct(id);
            if (result.IsFailure)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var res = _productManager.UpdateProduct(product);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(product);
            }
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var res = _productManager.AddProduct(product);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(product);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var res = _productManager.DisableProduct(id);
            if (res.IsFailure)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Enable(int id)
        {
            var res = _productManager.EnableProduct(id);
            if (res.IsFailure)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return RedirectToAction("List");
        }
    }
}
