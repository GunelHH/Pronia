using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProniaApp.DAL;
using ProniaApp.Extensions;
using ProniaApp.Models;

namespace ProniaApp.Areas.ProniaAdmin.Controllers
{
    [Area("proniaadmin")]
    public class SliderController:Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment env;

        public SliderController(ApplicationDbContext context,IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public IActionResult Index()
        {
            List<Slider> model = context.Sliders.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();


            if (slider.Photo is null)
            {
                ModelState.AddModelError("Photo", "Please choose image file");
                return View();
            }

            if (!slider.Photo.ImageIsOk(2))
            {
                ModelState.AddModelError("Photo","Please choose valid image file");
                return View();
            }


            slider.Image =await slider.Photo.FileCreate(env.WebRootPath,"assets/images/slider");
            await context.Sliders.AddAsync(slider);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Slider slider = context.Sliders.FirstOrDefault(s => s.Id == id);
            if (id is null) return NotFound();

            return View(slider);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Update(int? id,Slider slider)
        {
           
            Slider existed = await context.Sliders.FindAsync(id);
            if (existed is null) return NotFound();
            if (!ModelState.IsValid) return View(existed);
            if (slider.Photo==null)
            {
                string fileName = existed.Image;
                context.Entry(existed).CurrentValues.SetValues(slider);
                slider.Image = fileName;
            }
            else
            {
                if (!slider.Photo.ImageIsOk(2))
                {
                    ModelState.AddModelError("Photo", "Please choose valid image file");
                    return View(existed);
                }
                FileValidator.DeleteFile(env.WebRootPath, "assets/images/slider",existed.Image);
                context.Entry(existed).CurrentValues.SetValues(slider);
                existed.Image = await slider.Photo.FileCreate(env.WebRootPath, "assets/images/slider");
            }

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Slider slider =await context.Sliders.FindAsync(id);
            if (id is null) return NotFound();

            context.Sliders.Remove(slider);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

