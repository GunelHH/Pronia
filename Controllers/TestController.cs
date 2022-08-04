using System;
using Microsoft.AspNetCore.Mvc;

namespace ProniaApp.Controllers
{
    public class TestController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

