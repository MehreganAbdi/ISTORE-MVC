using I_STORE.Models;

namespace I_STORE.Interfaces
{
    public interface ISneakerService
    {
        Task<IEnumerable<Sneaker>> GetAll();
        Task<IEnumerable<Sneaker>> GetByNumericSize(int numericSize , string SneakerName);
        Task<IEnumerable<Sneaker>> GetByName(string sneakerName);
        bool Add(Sneaker sneaker);
        bool Update(Sneaker sneaker);
        bool Remove(Sneaker sneaker);
        bool Save();

    }
}
