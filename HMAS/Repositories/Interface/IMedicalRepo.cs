using HMAS.Models;

namespace HMAS.Repositories.Interface
{
    public interface IMedicalRepo
    {
        Task AddRecord(Records medicalRecord);
        Task<IEnumerable<Records>> GetAllRecords();
        Task<Records?> GetRecord(int id);
    }
}
