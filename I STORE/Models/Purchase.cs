using I_STORE.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I_STORE.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }
        [ForeignKey("SneakerId")]
        public int? SneakerId { get; set; } 
        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        [ForeignKey("AppUserId")]
        public string AppUserId { get; set; }
        public Status Status { get; set; }

    }
}
