using I_STORE.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace I_STORE.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int Cost { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string? Size { get; set; }
    }   
}
