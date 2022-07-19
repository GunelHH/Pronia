using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
	public class Category:BaseEntity
	{
        [Required]
        public string Name { get; set; }

        public List<PlantCategory> PlantCategories { get; set; }


    }
}

