﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace ProniaApp.Controllers
{
	public class HomeController:Controller
	{
		public IActionResult Index()
        {
			return View();
        }
	}
}
