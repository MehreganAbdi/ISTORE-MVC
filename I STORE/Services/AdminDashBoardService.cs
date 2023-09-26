using I_STORE.Data;
using I_STORE.Data.Enum;
using I_STORE.Interfaces;
using I_STORE.Models;
using I_STORE.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace I_STORE.Services
{
    public class AdminDashBoardService : IAdminDashBoardService
    {
        private readonly ApplicationDbContext _context;
        public AdminDashBoardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AcceptPurchase(Purchase purchase)
        {
            if (purchase.SneakerId == null)
            {
                var product =  _context.Products.FirstOrDefault(i => i.ProductID == purchase.ProductId);
                if (product.Count > 0)
                {
                    product.Count--;
                    Save();
                    purchase.Status = Data.Enum.Status.Done;
                    return Save();
                }
                purchase.Status = Data.Enum.Status.NotAvailable;

            }
            return Save();
        }

        public async Task<IEnumerable<PurchaseVM>> GetAllPurchases()
        {
            var model = new List<PurchaseVM>();
            var allPurchases = await _context.Purchases.ToListAsync();
            foreach (var item in allPurchases)
            {
                model.Add(await GetPurchaseDetail(item));
            }
            return model;
        }

        public async Task<IEnumerable<PurchaseVM>> GetAllPurchasesByUserIdAsunc(string userId)
        {
            var purchase = await _context.Purchases.Where(u => u.AppUserId == userId).ToListAsync();
            var data = new List<PurchaseVM>();
            foreach (var item in purchase)
            {
                data.Add(await GetPurchaseDetail(item));
            }
            return data;
        }

        public async Task<Purchase> GetByIdAsync(int purchaseId)
        {
            return await _context.Purchases.FirstOrDefaultAsync(i => i.PurchaseId == purchaseId);
        }

        public async Task<PurchaseVM> GetPurchaseDetail(Purchase purchase)
        {
            var user = await _context.Users.Include(i=>i.Address).Where(p => p.Id == purchase.AppUserId).FirstOrDefaultAsync();
            if (purchase.ProductId == null)
            {
                var sneaker = await _context.Sneakers.Where(s => s.SneakerId == purchase.SneakerId).FirstOrDefaultAsync();
                var purchaseVM = new PurchaseVM()
                {
                    User=user,
                    Sneaker = sneaker
                };
                return purchaseVM;
            }
            else
            {
                var product = await _context.Products.Where(p => p.ProductID == purchase.ProductId).FirstOrDefaultAsync();
                var purchaseVM2 = new PurchaseVM()
                {
                    User = user,
                    Product = product
                };

                return purchaseVM2;
            }
        }

        public bool RejectPurchase(Purchase purchase)
        {
            purchase.Status = Status.NotAvailable;
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
