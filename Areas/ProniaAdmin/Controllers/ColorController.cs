using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApp.DAL;
using ProniaApp.Models;

namespace ProniaApp.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class ColorController:Controller
    {
        private readonly ApplicationDbContext context;

        public ColorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<Color> colors = context.Colors.ToList();
            return View(colors);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Color color)
        {
              if (!ModelState.IsValid)
                {
                    return View();
                }
                Color existColor = await context.Colors.FirstOrDefaultAsync(c=>c.ColorName==color.ColorName);
                if (existColor != null)
                {
                    ModelState.AddModelError("Name", "The Size Already exist");
                    return View();
                }
                context.Colors.Add(color);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            

        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Color color = await context.Colors.FirstOrDefaultAsync(c => c.Id == id);
            if (color is null) return NotFound();
            return View(color);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id,Color Newcolor)
        {
            if (id is null || id == 0) return NotFound();
            if (!ModelState.IsValid) return View();
            Color ExistColorId = await context.Colors.FirstOrDefaultAsync(c => c.Id == id); 
            if (ExistColorId == null) return NotFound();
            bool ExistColorName =await context.Colors.AllAsync(i => i.ColorName == Newcolor.ColorName);

            if (ExistColorName)
            {
                ModelState.AddModelError("Name", "Already Exist");
                return View();
            }

            context.Entry(ExistColorId).CurrentValues.SetValues(Newcolor);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Color color = await context.Colors.FindAsync(id);

            if (color == null) return NotFound();

            context.Colors.Remove(color);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

