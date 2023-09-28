using System.ComponentModel.DataAnnotations;

namespace Context.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string FullAddress { get; set; }
    }
}
