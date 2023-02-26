using Microsoft.AspNetCore.Mvc;

namespace ServiceTrackingAutomation.App.Controllers
{
    public class ServiceActionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
