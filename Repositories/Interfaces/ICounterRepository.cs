using QLessProject.Models;

namespace QLessProject.Repositories.Interfaces
{
    public interface ICounterRepository
    {
        Task<IEnumerable<Counter>> GetAllAsync();

        Task<IEnumerable<Counter>> GetByDepartmentIdAsync(int departmentId);

        Task<Counter?> GetByIdAsync(int id);

        Task<int> AddAsync(Counter counter);

        Task<bool> UpdateAsync(Counter counter);

        Task<bool> DeleteAsync(int id);
    }
}