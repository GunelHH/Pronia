using System;
using System.Collections.Generic;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }

        public List<PlantTag> PlantTags { get; set; }

    }
}

