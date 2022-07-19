using System;
using System.ComponentModel.DataAnnotations;
using ProniaApp.Models.Base;

namespace ProniaApp.Models
{
    public class Color:BaseEntity
    {
        [Required]
        public string ColorName { get; set; }

    }
}

