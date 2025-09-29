using HMAS.DTO.Departments;
using HMAS.DTO.Doctors;
using HMAS.Models;

namespace HMAS.Services.Interface
{
    public interface IDeptService
    {
        Task<DepartmentReadDTO> AddDept(DepartmentDTO deptDTO);
        Task<IEnumerable<DepartmentReadDTO>> GetAll();
        Task<DepartmentReadDTO?> GetById(int id);
        Task<DepartmentReadDTO?> UpdateDept(int id, DepartmentDTO deptDTO);
        Task<bool> DeleteDept(int id);
    }
}
