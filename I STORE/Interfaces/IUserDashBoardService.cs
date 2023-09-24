using I_STORE.Models;
using I_STORE.ViewModels;

namespace I_STORE.Interfaces
{
    public interface IUserDashBoardService
    {
        Task<IEnumerable<Purchase>> GetPurchasesByUserIdAsync(string UserId);
        Task<Purchase> GetPurchaseById(int Id);
        Task<Purchase> GetPurchaseByIdNoTracking(int Id);
        Task<AppUser> GetUserById(string Id);
        Task<PurchaseVM> GetPurchaseVMByPuchase(Purchase purchase);
        Task<IEnumerable<PurchaseVM>> GetPurchasesWithDetailsByUserIdAsync(string UserId);
        bool UpdateUser(AppUser user);
        Task<AppUser> GetUserByIdNoTracking(string Id);
        bool AddPurchase(Purchase purchase);
        bool RemovePurchase(Purchase purchsase);
        bool UpdatePurchase(Purchase purchase);
        bool Save();

    }
}
