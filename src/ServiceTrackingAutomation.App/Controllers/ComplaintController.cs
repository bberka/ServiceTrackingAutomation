using System.Net;
using EasMe.Logging;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.App.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly IComplaintManager _complaintManager;
        private static readonly IEasLog logger = EasLogFactory.CreateLogger();

        public ComplaintController(IComplaintManager complaintManager)
        {
            _complaintManager = complaintManager;
        }
        [HttpGet]
        public IActionResult List()
        {
            var res = _complaintManager.GetComplaints();
            return View(res);
        }

        [HttpPost]
        public IActionResult CustomerComplaints(int id)
        {
            var res = _complaintManager.GetCustomerComplaints(id);
            return View(res);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Complaint complaint)
        {
            var res = _complaintManager.AddComplaint(complaint);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(complaint);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var res = _complaintManager.GetComplaint(id);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View();
            }
            return View(res.Data);
        }

        [HttpPost]
        public IActionResult Edit(Complaint complaint)
        {
            var res = _complaintManager.UpdateComplaint(complaint);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(complaint);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var res = _complaintManager.DeleteComplaint(id);
            if (res.IsFailure)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return RedirectToAction("List");
        }

     

        [HttpGet]
        public IActionResult Details(int id)
        {
            var res = _complaintManager.GetComplaint(id);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View();
            }
            return View(res.Data);
        }

    }
}
