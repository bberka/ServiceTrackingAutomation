using System.Net;
using EasMe.Logging;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackingAutomation.App.Filters;
using ServiceTrackingAutomation.Application.Manager;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Domain.Models;

namespace ServiceTrackingAutomation.App.Controllers
{
    [AuthFilter]
    public class ComplaintController : Controller
    {
        private readonly IComplaintManager _complaintManager;
        private readonly ICustomerManager _customerManager;
        private static readonly IEasLog logger = EasLogFactory.CreateLogger();

        public ComplaintController(
            IComplaintManager complaintManager,
            ICustomerManager customerManager)
        {
            _complaintManager = complaintManager;
            _customerManager = customerManager;

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
            var viewModel = new CreateComplaintModel();
            viewModel.Customers = _customerManager.GetCustomers();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(CreateComplaintModel model)
        {
            var res = _complaintManager.AddComplaint(model.Dto);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(model);
            }
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult CreateComplaintAndCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateComplaintAndCustomer(CreateComplaintAndCustomerModel model)
        {
            var res = _complaintManager.CreateComplaintAndCustomer(model);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(model);
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
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return View(res.Data);
        }

        [HttpGet]
        public IActionResult SentToCustomer(int id)
        {
            var model = new SentToCustomerModel()
            {
                ComplaintId = id,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SentToCustomer(SentToCustomerModel model)
        {
            var res = _complaintManager.SentToCustomer(model);
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(model);
            }
            return RedirectToAction("List");
        }

   

        [HttpGet]
        public IActionResult SentToService(int id)
        {
            var complaint = _complaintManager.GetComplaint(id);
            if (complaint.IsFailure) return StatusCode(500);
            var model = new ServiceUpdateModel()
            {
                ServiceNote = complaint.Data.ServiceNote,
                ComplaintId = id,
                Note = complaint.Data.Note,
                Description = complaint.Data.Description,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult SentToService(ServiceUpdateModel model)
        {
            var res = _complaintManager.SentToService(model);
            if (res.IsFailure) return StatusCode(500);
            return RedirectToAction("List");
        }
      

        [HttpGet]
        public IActionResult ReceivedFromService(int id)
        {
            var complaint = _complaintManager.GetComplaint(id);
            if (complaint.IsFailure) return StatusCode(500);
            var model = new ServiceUpdateModel()
            {
                ServiceNote = complaint.Data.ServiceNote,
                ComplaintId = id,
                Note = complaint.Data.Note,
                Description = complaint.Data.Description,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult ReceivedFromService(ServiceUpdateModel model)
        {
            var res = _complaintManager.ReceivedFromService(model);
            if (res.IsFailure) return StatusCode(500);
            return RedirectToAction("List");
        }
    }
}
