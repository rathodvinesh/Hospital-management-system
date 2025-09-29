using HMAS.DTO.Dashboard;

namespace HMAS.Services.Interface
{
    public interface IDashService
    {
        Task<IEnumerable<DailyAppoDTO>> GetDailyAppointmentsByDoctor();
        //Task<IEnumerable<DoctorUtilizationDTO>> GetDoctorUtilization();
        Task<IEnumerable<PatientFrequencyDTO>> GetPatientVisitFrequency();

    }
}
