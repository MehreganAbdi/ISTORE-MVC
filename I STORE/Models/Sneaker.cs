using I_STORE.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace I_STORE.Models
{
    public class Sneaker
    {
        [Key]
        public int SneakerId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public Company Company { get; set; }
    }
}
