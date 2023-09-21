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
        public int? NumericSize { get; set; }
        public string? AlphabeticalSize { get; set; }
    }   
}
