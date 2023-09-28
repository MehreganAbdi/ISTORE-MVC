using Context.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Context.Models
{
    public class Sneaker
    {
        [Key]
        public int SneakerId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public string? Image { get; set; }
        public int Count { get; set; }
        public Company Company { get; set; }
    }
}
