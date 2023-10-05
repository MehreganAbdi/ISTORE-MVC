using Context.Models;
using Context.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
        Task<IEnumerable<Event>> GetAllAsync();
        Event GetById(int Id);
        Task<Event> GetByIdAsync(int Id);
        Task<Event> GetByIdAsyncAsNoTracking(int Id);
        bool Add(Event eventt);
        Task<bool> AddAsync(Event eventt);
        bool Remove(Event eventt);
        Task<bool> RemoveAsync(Event eventt);
        bool Save();
        Task<bool> SaveAsync();
        bool Update(Event eventt);
        Task<IEnumerable<Sneaker>> GetSneakers();
        Task<IEnumerable<Product>> GetProducts();
    }
}
