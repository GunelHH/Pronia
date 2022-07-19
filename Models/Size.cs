using System;
using System.ComponentModel.DataAnnotations;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
    public class Size:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}

