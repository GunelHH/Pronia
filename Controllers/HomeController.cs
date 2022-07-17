using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApp.DAL;
using ProniaApp.Models;
using ProniaApp.ViewModels;

namespace ProniaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {

            HomeVM homeVM = new HomeVM()
            {
                sliders = context.Sliders.ToList(),
                plants = context.Plants.Include(i => i.Images).ToList()
            };
            return View(homeVM);
        }
    }
}

