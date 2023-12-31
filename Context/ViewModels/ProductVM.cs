﻿using Context.Data.Enum;
using Microsoft.AspNetCore.Http;

namespace Context.ViewModels
{
    public class ProductVM
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public IFormFile? Image { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string? Size { get; set; }
        public int? OFF { get; set; }
    }
}
