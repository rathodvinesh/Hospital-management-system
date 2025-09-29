using HMAS.DTO.Medical_Record;
using HMAS.Models;

namespace HMAS.Services.Interface
{
    public interface IMedicalService
    {
        Task AddRecord(MedicalDTO medicalRecord);
        Task<IEnumerable<MedicalReadDTO>> GetAllRecords();
        Task<MedicalReadDTO?> GetRecord(int id);
    }
}
