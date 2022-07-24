using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProniaApp.DAL;
using ProniaApp.Models;

namespace ProniaApp.Areas.Controllers
{
    [Area("ProniaAdmin")]
    public class CategoryController:Controller
    {
        private readonly ApplicationDbContext context;

        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "You can not duplicate category name");
                return View();
            }
          
            Category exist = context.Categories.FirstOrDefault(i => i.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (exist != null)
            {
                ModelState.AddModelError("Name", "The Category already exist");
                return View();
            }
            context.Categories.Add(category);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
           
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Category category = context.Categories.FirstOrDefault(i => i.Id == id);
            if (category is null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public  IActionResult Update(int? id,Category newCategory)
        {
            Category category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (!ModelState.IsValid) return View();
            if (category is null) return NotFound();
            Category ExistName = context.Categories.FirstOrDefault(c=>c.Name==newCategory.Name);
            if (ExistName!=null && ExistName.Id!=id)
            {
                ModelState.AddModelError("Name", "The category is already exist!");
                return View();
            }
            context.Entry(category).CurrentValues.SetValues(newCategory);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        } 

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Category category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null) return NotFound();
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

