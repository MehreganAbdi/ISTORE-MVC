using Context.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Context.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string? Image { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string? Size { get; set; }
        public int? OFF { get; set; }
    }   
}
