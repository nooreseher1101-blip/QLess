using QLessProject.Models;

namespace QLessProject.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();

        Task<Department?> GetByIdAsync(int id);

        Task<int> AddAsync(Department department);

        Task<bool> UpdateAsync(Department department);

        Task<bool> DeleteAsync(int id);
    }
}