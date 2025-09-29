using AutoMapper;
using HMAS.DTO.Departments;
using HMAS.Models;
using HMAS.Repositories.Interface;
using HMAS.Services.Interface;

namespace HMAS.Services
{
    public class DepartmentService : IDeptService
    {
        private readonly IDepartmentRepo _deptRepo;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepo deptRepo, IMapper mapper)
        {
            _deptRepo = deptRepo;
            _mapper = mapper;
        }
        public async Task<DepartmentReadDTO> AddDept(DepartmentDTO deptDTO)
        {
            var dept = _mapper.Map<Department>(deptDTO);
            await _deptRepo.AddDept(dept);
            return _mapper.Map<DepartmentReadDTO>(dept);
        }

        public async Task<bool> DeleteDept(int id)
        {
            return await _deptRepo.DeleteDept(id);
        }

        public async Task<IEnumerable<DepartmentReadDTO>> GetAll()
        {
            var dept = await _deptRepo.GetAll();
            return _mapper.Map<IEnumerable<DepartmentReadDTO>>(dept);
        }

        public async Task<DepartmentReadDTO?> GetById(int id)
        {
            var dept = await _deptRepo.GetById(id);
            return _mapper.Map<DepartmentReadDTO>(dept);
        }

        public async Task<DepartmentReadDTO?> UpdateDept(int id, DepartmentDTO deptDTO)
        {
            var deptMap = _mapper.Map<Department>(deptDTO);
            var dept = await _deptRepo.UpdateDept(id,deptMap);
            return _mapper.Map<DepartmentReadDTO>(dept);
        }
    }
}
