using I_STORE.Data;
using I_STORE.Interfaces;
using I_STORE.Models;
using I_STORE.ViewModels;
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
           
            if (purchase.ProductId != null)
            {
                var product =  _context.Products.Where(p => p.ProductID == purchase.ProductId).FirstOrDefault();
                if (product.Count > 0)
                {
                    _context.Purchases.Add(purchase);
                    product.Count--;
                    return Save();
                }
                return false;
            }
            else
            {
                var sneaker = _context.Sneakers.Where(p => p.SneakerId == purchase.SneakerId).FirstOrDefault();
                if (sneaker.Count > 0)
                {
                    _context.Purchases.Add(purchase);
                    sneaker.Count--;
                    return Save();
                }
                return false;
            }
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

        public async Task<AppUser> GetUserById(string Id)
        {
            return await _context.Users.Include(p=>p.Address).Where(u => u.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByIdNoTracking(string Id)
        {
            return await _context.Users.Include(p => p.Address).AsNoTracking().Where(u => u.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PurchaseVM>> GetPurchasesWithDetailsByUserIdAsync(string UserId)
        {
            var purchases = await _context.Purchases.Where(p => p.AppUserId == UserId).ToListAsync();
            var model = new List<PurchaseVM>();
            foreach (var item in purchases)
            {
                model.Add(await GetPurchaseVMByPuchase(item));
            }
            return model;
        }
        public async Task<PurchaseVM> GetPurchaseVMByPuchase(Purchase purchase)
        {
            var user = await _context.Users.Where(u => u.Id == purchase.AppUserId).FirstOrDefaultAsync();
            if (purchase.SneakerId != null)
            {
                var sneaker = await _context.Sneakers.Where(s => s.SneakerId == purchase.SneakerId).FirstOrDefaultAsync();
                var purchaseVM = new PurchaseVM()
                {
                    Sneaker = sneaker,
                    PurchaseId = purchase.PurchaseId
                };
                return purchaseVM;
            }
            var product = await _context.Products.Where(p => p.ProductID == purchase.ProductId).FirstOrDefaultAsync();
            var purchaseVM2 = new PurchaseVM()
            {
                Product = product,
                PurchaseId = purchase.PurchaseId
            };
            return purchaseVM2;

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

        public bool UpdateUser(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
    }
}
