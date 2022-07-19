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
    public class SizeController:Controller
    {
        private readonly ApplicationDbContext context;

        public SizeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<Size> model = context.Sizes.OrderByDescending(x => x.Id).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

       public async Task<IActionResult> Create(Size size)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Size existSize = await context.Sizes.FirstOrDefaultAsync(s=>s.Name==size.Name);
            if (existSize!=null)
            {
                ModelState.AddModelError("Name", "The Size Already exist");
                return View();
            }
            context.Sizes.Add(size);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Size size = await context.Sizes.FirstOrDefaultAsync(i => i.Id == id);
            if (size is null) return NotFound();
            return View(size);
           
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Update(int? id,Size Newsize)
        {
            if (id is null || id == 0) return NotFound();

            if (!ModelState.IsValid) return View();

            Size existSize=await context.Sizes.FirstOrDefaultAsync(s => s.Id == id);

            if (existSize is null)
            {
                ModelState.AddModelError("Name", "There is no such Size");
                return View();
            }

            bool Same = context.Sizes.Any(i => i.Name == Newsize.Name);
            if (Same)
            {
                ModelState.AddModelError("Name", "Already has such size");
                return View();
            }
            context.Entry(existSize).CurrentValues.SetValues(Newsize);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Size size = await context.Sizes.FirstOrDefaultAsync(i=>i.Id==id);
            if (size is null) return NotFound();

            context.Sizes.Remove(size);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

