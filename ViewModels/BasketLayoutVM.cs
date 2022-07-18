using System;
using System.Collections.Generic;

namespace ProniaApp.ViewModels
{
	public class BasketLayoutVM
	{
        public List<BasketItemVM> BasketItemVMs { get; set; }

        public decimal TotalPrice { get; set; }
    }
}


