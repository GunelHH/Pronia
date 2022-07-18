using System;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
	public class Setting:BaseEntity
	{
        public string Key { get; set; }

        public string Value { get; set; }

    }
}

