using TrainComponentManager.API.Models;

namespace TrainComponentManager.API.Interfaces
{
    public interface IComponentRepository
    {
        Task CreateAsync(Component component);
        Task<(IEnumerable<Component>, int)> GetAllAsync(int pageNumber, int pageSize);
        Task<Component?> GetAsync(int id);
        Task<Component?> UpdateAsync(Component component);
        Task<bool> DeleteAsync(int id);
        Task<(IEnumerable<Component>, int)> SearchAsync(string searchTerm, int pageNumber, int pageSize);
    }
}
