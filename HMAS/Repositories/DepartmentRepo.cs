using HMAS.Data;
using HMAS.Models;
using HMAS.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HMAS.Repositories
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDept(Department department)
        {
            await _context.Department.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteDept(int id)
        {
            var dept = await _context.Department.FindAsync(id);
            if (dept == null) return false;
            _context.Department.Remove(dept);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Department
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Department?> GetById(int id)
        {
            return await _context.Department.FindAsync(id);
        }

        public async Task<Department?> UpdateDept(int id, Department department)
        {
            var dept = await _context.Department.FindAsync(id);
            if (dept == null) return null;
            dept.DepartmentName = department.DepartmentName;
            await _context.SaveChangesAsync();
            return dept;
        }
    }
}
