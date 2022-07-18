using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProniaApp.DAL;
using ProniaApp.Models;
using ProniaApp.ViewModels;

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
            Plant plant = await context.Plants.Include(i=>i.Images).Include(i=>i.PlantInformation).Include(i=>i.PlantTags).ThenInclude(t=>t.Tag).Include(p => p.PlantCategories).ThenInclude(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==id);
            if (plant == null) return NotFound();
            ViewBag.Plants = await context.Plants.Include(p => p.Images).Include(c=>c.PlantCategories).ToListAsync();
            return View(plant);
        }

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Plant plant =await context.Plants.FirstOrDefaultAsync(i => i.Id == id);
            if (plant == null) return NotFound();

            string BasketStr = HttpContext.Request.Cookies["Basket"];

            BasketVM basket;

            if (string.IsNullOrEmpty(BasketStr))
            {

                basket = new BasketVM();

                BasketCookieItemVM basketCookie = new BasketCookieItemVM()
                {
                    Id=plant.Id,
                    Quantity=1
                };

                basket.BasketCookieItemVMs = new List<BasketCookieItemVM>();
                basket.BasketCookieItemVMs.Add(basketCookie);
                basket.TotalPrice = plant.Price;
            }
            else
            {
                basket = JsonConvert.DeserializeObject<BasketVM>(BasketStr);
                BasketCookieItemVM existed = basket.BasketCookieItemVMs.Find(i => i.Id == id);
                if (existed == null)
                {
                    BasketCookieItemVM cookieItem = new BasketCookieItemVM()
                    {
                        Id = plant.Id,
                        Quantity = 1
                    };
                    basket.BasketCookieItemVMs.Add(cookieItem);
                    basket.TotalPrice += plant.Price;
                }
                else
                {
                    basket.TotalPrice += plant.Price;
                    existed.Quantity++;
                }

            }
            BasketStr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", BasketStr);
            
            return RedirectToAction("index","home");
        }

        public IActionResult ShowBasket()
        {
            if (HttpContext.Request.Cookies["Basket"] == null) return NotFound();
            BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(HttpContext.Request.Cookies["Basket"]);
            return Json(basket);
        }


        public BasketLayoutVM RemoveFromBasket(int id)
        {
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            BasketLayoutVM layoutbasket = new BasketLayoutVM();
            foreach (BasketItemVM item in basketItems)
            {
                if (item.Plant.Id == id)
                {
                    basketItems.Remove(item);
                    layoutbasket.TotalPrice -= item.Plant.Price;
                }
            }
            return layoutbasket;

        }
    }
}

