using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
    public class Plant: BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Desc { get; set; }

        public string SKU { get; set; }

        public int? PlantInformationId { get; set; }

        public PlantInformation PlantInformation { get; set; }

        public List<PlantCategory> PlantCategories { get; set; }

        public List<Image> Images { get; set; }

        public List<PlantTag> PlantTags { get; set; }


        [NotMapped]
        public List<int> PlantCategoryIds { get; set; }

        [NotMapped]
        public List<int> PlantTagIds { get; set; }


        [NotMapped]
        public IFormFile MainPhoto { get; set; }

        [NotMapped]
        public IFormFile HoverPhoto { get; set; }

        [NotMapped]
        public List<IFormFile> Photos { get; set; }      
    }
}

