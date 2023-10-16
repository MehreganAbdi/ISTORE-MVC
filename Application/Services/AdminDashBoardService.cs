using Context.Data;
using Context.Data.Enum;
using Application.Interfaces;
using Context.Models;
using Context.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
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
                var product = _context.Products.AsNoTracking().FirstOrDefault(i => i.ProductID == purchase.ProductId);
                if (product.Count > 0)
                {
                    product.Count--;
                    _context.Update(product);
                    var user = _context.Users.FirstOrDefault(s => s.Id == purchase.AppUserId);
                    if (user.CartTotalCost == null)
                    {
                        user.CartTotalCost = product.Price;
                        _context.Update(user);
                    }
                    else
                    {
                        user.CartTotalCost += product.Price;
                        _context.Update(user);

                    }
                    purchase.Status =Status.Done;
                    return Save();
                }
                purchase.Status = Status.NotAvailable;
                return Save();

            }
            var sneaker = _context.Sneakers.FirstOrDefault(s => s.SneakerId == purchase.SneakerId);
            if (sneaker.Count > 0)
            {
                sneaker.Count--;
                _context.Update(sneaker);
                var user = _context.Users.FirstOrDefault(s => s.Id == purchase.AppUserId);

                if (user.CartTotalCost == null)
                {
                    user.CartTotalCost = sneaker.Price;
                    _context.Update(user);
                }
                else
                {
                    user.CartTotalCost += sneaker.Price;
                    _context.Update(user);

                }
                purchase.Status = Status.Done;
                return Save();
            }
            purchase.Status = Status.NotAvailable;
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
            var user = await _context.Users.Include(i => i.Address).FirstOrDefaultAsync(p => p.Id == purchase.AppUserId);
            if (purchase.ProductId == null)
            {
                var sneaker = await _context.Sneakers.Where(s => s.SneakerId == purchase.SneakerId).FirstOrDefaultAsync();
                var purchaseVM = new PurchaseVM()
                {
                    PurchaseId = purchase.PurchaseId,
                    User = user,
                    Sneaker = sneaker,
                    Status = purchase.Status
                };
                return purchaseVM;
            }
            else
            {
                var product = await _context.Products.Where(p => p.ProductID == purchase.ProductId).FirstOrDefaultAsync();
                var purchaseVM2 = new PurchaseVM()
                {
                    PurchaseId = purchase.PurchaseId,
                    User = user,
                    Product = product,
                    Status = purchase.Status

                };

                return purchaseVM2;
            }
        }

        public Task<AppUser> GetUserByIdAsync(string Id)
        {
            return _context.Users.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public bool OffForAllProducts(int offPercentage)
        {
            foreach (var item in _context.Products)
            {
                item.OFF = offPercentage;
              
            }

            return Save();
        }

        public bool OffForAllSneakers(int offPercentage)
        {
            foreach (var item in _context.Sneakers)
            {
                item.OFF = offPercentage;
               
            }

            return Save();
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

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false; 
        }
    }
}
