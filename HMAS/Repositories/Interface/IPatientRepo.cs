using HMAS.Models;

namespace HMAS.Repositories.Interface
{
    public interface IPatientRepo
    {
        Task AddPatient(Patient patient);
        Task<IEnumerable<Patient>> GetAll();
        Task<IEnumerable<Patient>> Search(string ketyword);
        Task<Patient?> GetById(int id);
    }
}
