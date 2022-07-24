using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApp.DAL;
using ProniaApp.Extensions;
using ProniaApp.Models;

namespace ProniaApp.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class PlantController:Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment env;

        public PlantController(ApplicationDbContext context,IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Plant> model =await context.Plants
                .Include(p => p.PlantInformation)
                .Include(p => p.PlantCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.Images)
                .Include(p => p.PlantTags).ThenInclude(pt => pt.Tag).ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories =await context.Categories.ToListAsync();
            ViewBag.Informations =await context.PlantInformations.ToListAsync();
            ViewBag.Tags =await context.Tags.ToListAsync();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Plant plant)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await context.Categories.ToListAsync();
                ViewBag.Informations = await context.PlantInformations.ToListAsync();
                ViewBag.Tags = await context.Tags.ToListAsync();
                return View();
            }
            if (plant.MainPhoto is null || plant.HoverPhoto is null || plant.Photos is null)
            {
                ViewBag.Categories = await context.Categories.ToListAsync();
                ViewBag.Informations = await context.PlantInformations.ToListAsync();
                ViewBag.Tags = await context.Tags.ToListAsync();
                ModelState.AddModelError(string.Empty, "You Must Choose one main photo,one one hover photo and one another photo");
                return View();
            }
            if (!plant.MainPhoto.ImageIsOk(2) || !plant.HoverPhoto.ImageIsOk(2))
            {
                ViewBag.Categories = await context.Categories.ToListAsync();
                ViewBag.Informations = await context.PlantInformations.ToListAsync();
                ViewBag.Tags = await context.Tags.ToListAsync();
                ModelState.AddModelError(string.Empty, "Choose valid Image file");
                return View();
            }
            plant.Images = new List<Image>();
            TempData["FileName"] = "";
            foreach (IFormFile photo in plant.Photos)
            {
                if (!photo.ImageIsOk(2))
                {
                    plant.Photos.Remove(photo);
                    TempData["FileName"] += photo.FileName + ",";
                    continue;
                }
                Image another = new Image
                {
                    Name = await photo.FileCreate(env.WebRootPath, "assets/images/website-images"),
                    IsActive = false,
                    Alternative = plant.Name,
                    Plant = plant
                };
                plant.Images.Add(another);
            }
            Image main = new Image
            {
                Name = await plant.MainPhoto.FileCreate(env.WebRootPath, "assets/images/website-images"),
                IsActive = true,
                Alternative = plant.Name,
                Plant=plant 
            };

            Image hover = new Image
            {
                Name = await plant.HoverPhoto.FileCreate(env.WebRootPath, "assets/images/website-images"),
                IsActive = null,
                Alternative = plant.Name,
                Plant=plant
            };

            plant.Images.Add(main);
            plant.Images.Add(hover);

            plant.PlantCategories = new List<PlantCategory>();
            foreach (int id in plant.PlantCategoryIds)
            {
                PlantCategory category = new PlantCategory
                {
                    CategoryId = id,
                    Plant = plant
                };
                plant.PlantCategories.Add(category);
            }

            await context.Plants.AddAsync(plant);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update()
        {
            return View();
        }

        //public async Task<IActionResult> Update(int? id,Plant plant)
        //{
        //    Plant exist = await context.Plants.FirstOrDefaultAsync(i=>i.);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}

