using HMAS.DTO.Appointments;
using HMAS.Models;

namespace HMAS.Repositories.Interface
{
    public interface IAppointRepo
    {
        Task AddApp(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAppo();
        Task<Appointment> GetAppoById(int id);
        Task<IEnumerable<Appointment>> GetAppoByKey(string keyword);
        Task<Appointment?> UpdateAppo(int id,Appointment appointment);
        Task<DoctorAvailability?> GetDoctorAvailabilityAsync(int doctorId, DayOfWeek day);
        Task<bool> IsDoctorOnLeaveAsync(int doctorId, DateOnly date);
        Task<bool> IsSlotTakenAsync(int doctorId, DateOnly date, TimeOnly time);
    }
}
