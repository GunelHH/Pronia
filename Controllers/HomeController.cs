using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProniaApp.DAL;
using ProniaApp.Models;

namespace ProniaApp.Controllers
{
	public class HomeController:Controller
	{
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }
    
		public IActionResult Index()
        {
            List<Slider> sliders = context.Sliders.ToList();
			return View(sliders);
        }
	}
}

