using I_STORE.Data.Enum;
using I_STORE.Models;

namespace I_STORE.Interfaces
{
    public interface ILowerBodyService
    {
        Task<IEnumerable<Product>> GetAllByCategory(ProductCategory category);
        Task<IEnumerable<Product>> GetByName(string Name);
        Task<Product> GetByIdAsync(int Id);
        Task<Product> GetByIdAsyncNoTracking(int Id);
        bool Add(Product product);
        bool Remove(Product product);
        bool Update(Product product);
        bool Save();
    }
}
