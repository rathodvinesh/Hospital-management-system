using HMAS.DTO.Dashboard;

namespace HMAS.Repositories.Interface
{
    public interface IDashRepo
    {
        Task<IEnumerable<DailyAppoDTO>> GetDailyAppointmentsByDoctor();
        //Task<IEnumerable<DoctorUtilizationDTO>> GetDoctorUtilization();
        Task<IEnumerable<PatientFrequencyDTO>> GetPatientVisitFrequency();

    }
}
