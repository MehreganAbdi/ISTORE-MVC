using I_STORE.Data.Enum;
using I_STORE.Models;

namespace I_STORE.Interfaces
{
    public interface IUpperBodyService
    {
        Task<IEnumerable<Product>> GetAllByCategory(ProductCategory category);
        Task<IEnumerable<Product>> GetByName(string Name);

    }
}
