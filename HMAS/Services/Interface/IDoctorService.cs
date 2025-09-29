using HMAS.DTO.Doctors;
using HMAS.DTO.Leaves;
using HMAS.Models;

namespace HMAS.Services.Interface
{
    public interface IDoctorService
    {
        Task<DoctorReadDTO> AddDoctor(DoctorDTO doctorDTO);
        Task<LeaveReadDTO> ApplyLeave(LeaveDTO leaveDTO);
        Task<IEnumerable<DoctorReadDTO>> GetAll();
        Task<DoctorReadDTO?> GetById(int id);
        Task<DoctorReadDTO?> UpdateDoctor(int id,DoctorDTO doctorDTO);
        Task<bool> DeleteDoctor(int id);
        Task<List<DoctorAvailReadDTO>> AddAvailabilityAsync(int doctorId, List<DoctorAvailDTO> availabilityDtos);
        Task<IEnumerable<DoctorAvailReadDTO>> GetAvailabilityAsync(int doctorId);
    }
}
