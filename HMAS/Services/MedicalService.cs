using AutoMapper;
using HMAS.DTO.Medical_Record;
using HMAS.Models;
using HMAS.Repositories.Interface;
using HMAS.Services.Interface;

namespace HMAS.Services
{
    public class MedicalService : IMedicalService
    {
        private readonly IMedicalRepo _medRepo;
        private readonly IMapper _mapper;

        public MedicalService(IMedicalRepo medRepo, IMapper mapper)
        {
            _medRepo = medRepo;
            _mapper = mapper;
        }
        public async Task AddRecord(MedicalDTO medicalRecord)
        {
            await _medRepo.AddRecord(_mapper.Map<Records>(medicalRecord));
        }

        public async Task<IEnumerable<MedicalReadDTO>> GetAllRecords()
        {
            var med = await _medRepo.GetAllRecords();
            return _mapper.Map<IEnumerable<MedicalReadDTO>>(med);
        }

        public async Task<MedicalReadDTO?> GetRecord(int id)
        {
            var record = await _medRepo.GetRecord(id);
            return _mapper.Map<MedicalReadDTO>(record);
        }
    }
}
