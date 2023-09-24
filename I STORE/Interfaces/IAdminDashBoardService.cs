using I_STORE.Models;
using I_STORE.ViewModels;

namespace I_STORE.Interfaces
{
    public interface IAdminDashBoardService
    {
        Task<IEnumerable<PurchaseVM>> GetAllPurchases();
        Task<IEnumerable<PurchaseVM>> GetAllPurchasesByUserIdAsunc(string userId);
        bool RejectPurchase(int purchaseId);
        Task<PurchaseVM> GetPurchaseDetail(Purchase purchase);
        
    }
}
