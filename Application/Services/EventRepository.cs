using Application.Interfaces;
using Context.Data;
using Context.Models;
using Context.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Event eventt)
        {
            _context.Events.Add(eventt);
            return Save();
        }

        public Task<bool> AddAsync(Event eventt)
        {
            _context.Events.AddAsync(eventt);
            return SaveAsync();
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.ToList();
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public Event GetById(int Id)
        {
            return _context.Events.FirstOrDefault(e => e.Id == Id);
        }

        public async Task<Event> GetByIdAsync(int Id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == Id);
        }

        public Task<Event> GetByIdAsyncAsNoTracking(int Id)
        {
            return _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Sneaker>> GetSneakers()
        {
            return await _context.Sneakers.ToListAsync();
        }

        public bool Remove(Event eventt)
        {
            _context.Remove(eventt);
            return Save();
        }

        public async Task<bool> RemoveAsync(Event eventt)
        {
            _context.Events.Remove(eventt);
            return await SaveAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public bool Update(Event eventt)
        {
            _context.Events.Update(eventt);
            return Save();
        }
    }
}
