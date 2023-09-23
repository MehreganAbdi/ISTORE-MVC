using I_STORE.Data;
using I_STORE.Data.Enum;
using I_STORE.Interfaces;
using I_STORE.Models;
using Microsoft.EntityFrameworkCore;

namespace I_STORE.Services
{
    public class UpperBodyService : IUpperBodyService
    {
        private readonly ApplicationDbContext _context;
        public UpperBodyService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int Id)
        {
            return await _context.Products.FirstAsync(p=>p.ProductID == Id);
        }

        public async Task<Product> GetByIdAsyncNoTracking(int Id)
        {
            return await _context.Products.AsNoTracking().FirstAsync(p => p.ProductID == Id);
        }

        public async Task<IEnumerable<Product>> GetByName(string Name)
        {
            return await _context.Products.Where(p => p.ProductName.Contains(Name)).ToListAsync();
        }

        public bool Remove(Product product)
        {
            _context.Products.Remove(product);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false; 
        }

        public bool Update(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
