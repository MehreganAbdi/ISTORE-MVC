using I_STORE.Data;
using I_STORE.Interfaces;
using I_STORE.Models;
using Microsoft.EntityFrameworkCore;

namespace I_STORE.Services
{
    public class SneakerService : ISneakerService
    {
        private readonly ApplicationDbContext _context;
        public SneakerService(ApplicationDbContext  context)
        {
            _context = context;
        }

        public bool Add(Product sneaker)
        {
            _context.Products.Add(sneaker);
            return Save();
                
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByName(string sneakerName)
        {
            return await _context.Products.Where(s => s.ProductName.Contains(sneakerName)).FirstOrDefaultAsync();
        }

        public async Task<Product> GetByNumericSize(int numericSize , string sneakerName)
        {
            return await _context.Products.Where(s => s.NumericSize == numericSize).FirstOrDefaultAsync();
        }

        public bool Remove(Product sneaker)
        {
            _context.Products.Remove(sneaker);
            return Save();

        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;

        }

        public bool Update(Product sneaker)
        {
            _context.Products.Update(sneaker);
            return Save();
        }
    }
}
