using System.ComponentModel.DataAnnotations;

namespace I_STORE.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string FullAddress { get; set; }
    }
}
