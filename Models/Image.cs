using System;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
	public class Image:BaseEntity
	{
        public string Name { get; set; }

        public string Alternative { get; set; } 

        public int PlantId { get; set; }

        public Plant Plant { get; set; }

        public  bool? IsActive { get; set; }
    }
}

