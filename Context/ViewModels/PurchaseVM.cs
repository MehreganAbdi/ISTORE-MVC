using Context.Data.Enum;
using Context.Models;

namespace Context.ViewModels
{
    public class PurchaseVM
    {
        public AppUser User { get; set; }
        public Product? Product { get; set; }
        public Sneaker? Sneaker { get; set; }
        public int PurchaseId { get; set; }
        public Status Status { get; set; }
    }
}
