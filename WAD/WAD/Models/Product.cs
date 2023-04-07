using System;

namespace WAD_12435.Models
{
    public class Product
    {
        public Category Category { get; set; }
        public int ID { get; set; }
        public int Price { get; set; }
        public bool isActive { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
