using HMAS.DTO.Patient;

namespace HMAS.Services.Interface
{
    public interface IPatientService
    {
        Task<PatientReadDTO> AddPatient(PatientDTO patientDto);
        Task<IEnumerable<PatientReadDTO>> GetAll();
        Task<IEnumerable<PatientReadDTO>> Search(string keyword);
        Task<PatientReadAppointDTO?> GetById(int id);
    }
}
