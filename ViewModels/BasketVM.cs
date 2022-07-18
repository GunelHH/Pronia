using System;
using System.Collections.Generic;
using ProniaApp.Models;

namespace ProniaApp.ViewModels
{
	public class BasketVM
	{
        public List<BasketCookieItemVM> BasketCookieItemVMs { get; set; }

        public decimal TotalPrice { get; set; }
    }
}

