﻿using Microsoft.AspNetCore.Mvc;

namespace Swipey.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
