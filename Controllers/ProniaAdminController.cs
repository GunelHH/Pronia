using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProniaApp.Controllers
{
    public class ProniaAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}