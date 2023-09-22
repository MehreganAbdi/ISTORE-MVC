using I_STORE.Models;

namespace I_STORE.Interfaces
{
    public interface ISneakerService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByNumericSize(int numericSize);
        Task<Product> GetByName(string sneakerName);
        bool Add(Product sneaker);
        bool Update(Product sneaker);
        bool Remove(Product sneaker);
        bool Save();

    }
}
