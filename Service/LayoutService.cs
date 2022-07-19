using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProniaApp.DAL;
using ProniaApp.Models;
using ProniaApp.ViewModels;

namespace ProniaApp
{
	public class LayoutService
	{
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor http;

        public LayoutService(ApplicationDbContext context,IHttpContextAccessor http)
		{
            this.context = context;
            this.http = http;
        }

        public List<Setting> GetSettings()
        {
            List<Setting> settings = context.Settings.ToList();
            return settings;
        }

        public BasketLayoutVM GetBasket()
        {

            string BasketStr = http.HttpContext.Request.Cookies["Basket"];

            if (!string.IsNullOrEmpty(BasketStr))
            {
                BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(BasketStr);
                BasketLayoutVM layoutBasket = new BasketLayoutVM();
                layoutBasket.BasketItemVMs = new List<BasketItemVM>();
                foreach (BasketCookieItemVM cookie in basket.BasketCookieItemVMs)
                {
                    Plant existed = context.Plants.Include(i => i.Images).FirstOrDefault(i => i.Id == cookie.Id);

                    if (existed == null)
                    {
                        basket.BasketCookieItemVMs.Remove(cookie);
                        continue;
                    }
                    BasketItemVM basketItemVM = new BasketItemVM()
                    {
                        Plant = existed,
                        Quantity=cookie.Quantity
                    };
                    layoutBasket.BasketItemVMs.Add(basketItemVM);

                }
                layoutBasket.TotalPrice += basket.TotalPrice;
                return layoutBasket;
            }
            return null;
        }

        


	}
}

