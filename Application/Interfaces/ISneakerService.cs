using Context.Models;

namespace Application.Interfaces
{
    public interface ISneakerService
    {
        Task<IEnumerable<Sneaker>> GetAll();
        Task<IEnumerable<Sneaker>> GetByNumericSize(int numericSize , string SneakerName);
        Task<IEnumerable<Sneaker>> GetByName(string sneakerName);
        Task<Sneaker> GetByIdAsync(int Id);
        Task<Sneaker> GetByIdAsyncNoTracking(int Id);
        bool Add(Sneaker sneaker);
        bool Update(Sneaker sneaker);
        bool Remove(Sneaker sneaker);
        bool Save();

    }
}
