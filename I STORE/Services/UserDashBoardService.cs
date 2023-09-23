using I_STORE.Data;
using I_STORE.Interfaces;
using I_STORE.Models;
using Microsoft.EntityFrameworkCore;

namespace I_STORE.Services
{
    public class UserDashBoardService : IUserDashBoardService
    {
        private readonly ApplicationDbContext _context;
        public UserDashBoardService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddPurchase(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            return Save();
        }

        public async Task<Purchase> GetPurchaseById(int Id)
        {
            return await _context.Purchases.Where(p => p.PurchaseId == Id).FirstOrDefaultAsync();
        }

        public async Task<Purchase> GetPurchaseByIdNoTracking(int Id)
        {
            return await _context.Purchases.AsNoTracking().Where(p => p.PurchaseId == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByUserIdAsync(string UserId)
        {
            return await _context.Purchases.Where(p => p.AppUserId == UserId).ToListAsync();
        }

        public bool RemovePurchase(Purchase purchase)
        {
            _context.Purchases.Remove(purchase);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool UpdatePurchase(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
            return Save();
        }
    }
}
