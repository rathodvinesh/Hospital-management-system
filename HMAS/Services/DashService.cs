using HMAS.DTO.Dashboard;
using HMAS.Repositories.Interface;
using HMAS.Services.Interface;

namespace HMAS.Services
{
    public class DashService : IDashService
    {
        private readonly IDashRepo _repo;

        public DashService(IDashRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<DailyAppoDTO>> GetDailyAppointmentsByDoctor()
        {
           return await _repo.GetDailyAppointmentsByDoctor();
        }

        //public async Task<IEnumerable<DoctorUtilizationDTO>> GetDoctorUtilization()
        //{
        //    return await _repo.GetDoctorUtilization();
        //}

        public async Task<IEnumerable<PatientFrequencyDTO>> GetPatientVisitFrequency()
        {
            return await _repo.GetPatientVisitFrequency();
        }
    }
}
