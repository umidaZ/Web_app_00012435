using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WAD_Final_12435.Models
{
    public class Product

    {
        public Category Category { get; set; }
        public int ID { get; set; }
        public int Price { get; set; }
        public bool isActive { get; set; }
        [Required(ErrorMessage = "The note field is required")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
