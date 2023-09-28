using Context.Data;
using Application.Interfaces;
using Context.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class SneakerService : ISneakerService
    {
        private readonly ApplicationDbContext _context;
        public SneakerService(ApplicationDbContext  context)
        {
            _context = context;
        }

        public bool Add(Sneaker sneaker)
        {
            _context.Sneakers.Add(sneaker);
            return Save();
                
        }

        public async Task<IEnumerable<Sneaker>> GetAll()
        {
            return await _context.Sneakers.ToListAsync();
        }

        public async Task<Sneaker> GetByIdAsync(int Id)
        {
            return await _context.Sneakers.FirstAsync(s => s.SneakerId == Id);
        }

        public async Task<Sneaker> GetByIdAsyncNoTracking(int Id)
        {
            return await _context.Sneakers.AsNoTracking().Where(b => b.SneakerId == Id).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Sneaker>> GetByName(string sneakerName)
        {
            return await _context.Sneakers.Where(s => s.Name.Contains(sneakerName)).ToListAsync();
        }

        public async Task<IEnumerable<Sneaker>> GetByNumericSize(int numericSize , string? sneakerName)
        {
            return await _context.Sneakers.Where(s => s.Size==numericSize && s.Name.Contains(sneakerName)).ToListAsync();
        }

        public bool Remove(Sneaker sneaker)
        {
            _context.Sneakers.Remove(sneaker);
            return Save();

        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;

        }

        public bool Update(Sneaker sneaker)
        {
            _context.Sneakers.Update(sneaker);
            return Save();
        }
    }
}
