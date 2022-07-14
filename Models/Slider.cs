using System;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
	public class Slider:BaseEntity
	{
        public string Image { get; set; }

        public string Discount { get; set; }

        public string Title { get; set; }

        public string Desc { get; set; }

        public string ButtonScript { get; set; }

        public string ButtonURL { get; set; }

        public byte Order { get; set; }
    }
}

