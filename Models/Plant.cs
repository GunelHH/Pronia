using System;
using System.Collections.Generic;
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
    }
}

