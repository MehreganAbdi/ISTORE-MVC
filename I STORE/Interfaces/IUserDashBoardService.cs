using I_STORE.Models;

namespace I_STORE.Interfaces
{
    public interface IUserDashBoardService
    {
        Task<IEnumerable<Purchase>> GetPurchasesByUserIdAsync(string UserId);
        Task<Purchase> GetPurchaseById(int Id);
        Task<Purchase> GetPurchaseByIdNoTracking(int Id);
        bool AddPurchase(Purchase purchase);
        bool RemovePurchase(Purchase purchsase);
        bool UpdatePurchase(Purchase purchase);
        bool Save();

    }
}
