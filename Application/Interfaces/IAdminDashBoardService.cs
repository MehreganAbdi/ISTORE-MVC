using Context.Models;
using Context.ViewModels;

namespace Application.Interfaces
{
    public interface IAdminDashBoardService
    {
        Task<IEnumerable<PurchaseVM>> GetAllPurchases();
        Task<Purchase> GetByIdAsync(int purchaseId);
        Task<IEnumerable<PurchaseVM>> GetAllPurchasesByUserIdAsunc(string userId);
        bool RejectPurchase(Purchase purchase);
        Task<AppUser> GetUserByIdAsync(string Id);
        Task<PurchaseVM> GetPurchaseDetail(Purchase purchase);
        bool AcceptPurchase(Purchase purchase);
        bool Save();

    }
}
