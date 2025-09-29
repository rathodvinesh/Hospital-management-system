using AutoMapper;
using HMAS.DTO.Patient;
using HMAS.Models;
using HMAS.Repositories.Interface;
using HMAS.Services.Interface;

namespace HMAS.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IMapper _mapper;
        public PatientService(IPatientRepo patientRepo, IMapper mapper) 
        {
            _mapper = mapper;
            _patientRepo = patientRepo;
        }

        public async Task<PatientReadDTO> AddPatient(PatientDTO patientDto)
        {
            //var pt = new PatientReadDTO
            //{
            //    PatientName = patientDto.PatientName,
            //    Email = patientDto.Email,
            //    DOB = patientDto.DOB,
            //    Gender = patientDto.Gender,
            //    PhoneNumber = patientDto.PhoneNumber,
            //};
            var pt = _mapper.Map<Patient>(patientDto);
            await _patientRepo.AddPatient(pt);


            return _mapper.Map<PatientReadDTO>(pt);
        }

        public async Task<IEnumerable<PatientReadDTO>> GetAll()
        {
            var pat = await _patientRepo.GetAll();
            var patRead = _mapper.Map<IEnumerable<PatientReadDTO>>(pat);
            return patRead;
        }

        public async Task<PatientReadAppointDTO?> GetById(int id)
        {
            var patient = await _patientRepo.GetById(id);
            return patient == null ? null : _mapper.Map<PatientReadAppointDTO>(patient);
        }

        public async Task<IEnumerable<PatientReadDTO>> Search(string keyword)
        {
            var pat = await _patientRepo.Search(keyword);
            var patRead = _mapper.Map<IEnumerable<PatientReadDTO>>(pat);
            return patRead;
        }
    }
}
