using System;
using System.Collections.Generic;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
	public class Category:BaseEntity
	{
        public string Name { get; set; }

        public List<PlantCategory> PlantCategories { get; set; }


    }
}

