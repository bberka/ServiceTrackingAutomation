using Microsoft.AspNetCore.Mvc;
using ServiceTrackingAutomation.App.Filters;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Domain.Models;

namespace ServiceTrackingAutomation.App.Controllers
{
    [AuthFilter]
    public class ServiceActionController : Controller
    {
        private readonly IServiceActionManager _serviceActionManager;
        private readonly IServiceManager _serviceManager;

        public ServiceActionController(IServiceActionManager serviceActionManager,IServiceManager serviceManager)
        {
            _serviceActionManager = serviceActionManager;
            _serviceManager = serviceManager;
        }
        [HttpGet]
        public IActionResult List()
        {
            var res = _serviceActionManager.GetServiceActions();
            return View(res);
        }

        [HttpGet]
        public IActionResult Create(int complaintId)
        {
            var res = _serviceActionManager.GetServiceActionByComplaintId(complaintId);
            var model = new CreateServiceActionModel();
            model.ComplaintId = complaintId;
            model.Services = _serviceManager.GetServices();
            if (res.IsSuccess)
            {
                model.Description = res.Data.Description;
                model.ServiceId = res.Data.ServiceId;
                model.SentToServiceDate = res.Data.SentToServiceDate;
                model.ReceivedFromServiceDate = res.Data.ReceivedFromServiceDate;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateServiceActionModel createServiceActionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createServiceActionModel);
            }
            var res = _serviceActionManager.AddServiceAction(createServiceActionModel.ToServiceAction());
            if (res.IsFailure)
            {
                ModelState.AddModelError("", res.ErrorCode);
                return View(createServiceActionModel);
            }
            return RedirectToAction("List");
        }
    }
}
