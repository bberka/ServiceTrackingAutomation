﻿using EasMe.Logging;
using Microsoft.AspNetCore.Mvc;

namespace ServiceTrackingAutomation.App.Controllers
{
    public class CustomerController : Controller
    {
        private static readonly IEasLog logger = EasLogFactory.CreateLogger();

        public IActionResult Index()
        {
            return View();
        }
    }
}