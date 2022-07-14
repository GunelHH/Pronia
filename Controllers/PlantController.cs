using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApp.DAL;
using ProniaApp.Models;

namespace ProniaApp.Controllers
{
	public class PlantController:Controller
	{
        private readonly ApplicationDbContext context;

        public PlantController(ApplicationDbContext context)
        {
            this.context = context;
        }
		public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id==0)
            {
                return NotFound();
            }
            Plant plant = await context.Plants.Include(p => p.PlantCategories).ThenInclude(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==id);
            if (plant == null) return NotFound();
        
                return View(plant);
        }
	}
}

