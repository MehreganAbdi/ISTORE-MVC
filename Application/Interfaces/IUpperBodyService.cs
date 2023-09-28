using Context.Data.Enum;
using Context.Models;

namespace Application.Interfaces
{
    public interface IUpperBodyService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetByName(string Name);
        Task<Product> GetByIdAsync(int Id);
        Task<Product> GetByIdAsyncNoTracking(int Id);
        bool Add(Product product);
        bool Remove(Product product);
        bool Update(Product product);
        bool Save();
    }
}
