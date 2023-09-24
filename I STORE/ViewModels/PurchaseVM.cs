using I_STORE.Models;

namespace I_STORE.ViewModels
{
    public class PurchaseVM
    {
        public Product? Product { get; set; }
        public Sneaker? Sneaker { get; set; }
        public int PurchaseId { get; set; }
    }
}
