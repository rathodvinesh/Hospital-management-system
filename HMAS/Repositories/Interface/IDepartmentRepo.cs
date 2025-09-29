using HMAS.Models;

namespace HMAS.Repositories.Interface
{
    public interface IDepartmentRepo
    {
        Task AddDept(Department department);
        Task<IEnumerable<Department>> GetAll();
        Task<Department?> GetById(int id);
        Task<Department?> UpdateDept(int id, Department department);
        Task<bool> DeleteDept(int id);
    }
}
