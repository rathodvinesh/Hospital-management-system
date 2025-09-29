using HMAS.DTO.Appointments;
using HMAS.Models;

namespace HMAS.Services.Interface
{
    public interface IAppointService
    {
        Task<AppointmentResult?> AddAppointment(AppointmentDTO appointmentDTO);
        Task<IEnumerable<AppointmentReadDTO>> GetAppo();
        Task<Appointment?> GetAppoById(int id);
        Task<IEnumerable<AppointmentReadDTO>> GetAppoByKey(string keyword);
        Task<AppointmentReadDTO> UpdateAppo(int id, AppointmentDTO appointmentReadDTO);
    }
}
