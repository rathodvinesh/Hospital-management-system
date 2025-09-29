using HMAS.DTO.Doctors;
using HMAS.Models;

namespace HMAS.Repositories.Interface
{
    public interface IDoctorRepo
    {
        Task AddDoctor(Doctor doctor);
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor?> GetById(int id);
        Task ApplyLeave(Leave leave);
        Task<Doctor?> UpdateDoctor(int id, Doctor doctor);
        Task<bool> DeleteDoctor(int id);
        Task AddAvailabilityAsync(int doctorId, List<DoctorAvailability> availabilityDTOs);
        Task<List<DoctorAvailability>> GetAvailabilityAsync(int doctorId);
        Task<bool> IsDoctorOnLeaveAsync(int doctorId, DateOnly date);
        Task<DoctorAvailability?> GetByDoctorAndDayAsync(int doctorId, DayOfWeek day);
    }
}
