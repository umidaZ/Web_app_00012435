using System.Collections.Generic;
using System;

namespace WAD_12435.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public CategoryType CategoryTypeEnum { get; set; }
        public DateTime CreatedAt { get; set; }

        public enum CategoryType
        {
            Man,
            Woman,
            Kid
        }


    }
}
