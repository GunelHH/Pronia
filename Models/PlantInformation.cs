using System;
using System.Collections.Generic;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
	public class PlantInformation:BaseEntity
	{
        public string Shipping { get; set; }

        public string AboutReturnRequest { get; set; }

        public string Guarantee { get; set; }

        public List<Plant> Plants { get; set; }
    }
}

