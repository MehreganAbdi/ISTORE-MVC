using I_STORE.Models;
using I_STORE.ViewModels;

namespace I_STORE.Interfaces
{
    public interface IAdminDashBoardService
    {
        Task<IEnumerable<PurchaseVM>> GetAllPurchases();
        Task<Purchase> GetByIdAsync(int purchaseId);
        Task<IEnumerable<PurchaseVM>> GetAllPurchasesByUserIdAsunc(string userId);
        bool RejectPurchase(Purchase purchase);
        Task<PurchaseVM> GetPurchaseDetail(Purchase purchase);
        bool AcceptPurchase(Purchase purchase);
        bool Save();

    }
}
