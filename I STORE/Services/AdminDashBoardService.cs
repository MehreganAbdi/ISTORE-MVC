using I_STORE.Data;
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

        public Task<IEnumerable<PurchaseVM>> GetAllPurchasesByUserIdAsunc(string userId)
        {
            throw new NotImplementedException();
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

        public bool RejectPurchase(int purchaseId)
        {
            throw new NotImplementedException();
        }
    }
}
