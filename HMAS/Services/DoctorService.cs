using AutoMapper;
using HMAS.DTO.Doctors;
using HMAS.DTO.Leaves;
using HMAS.Models;
using HMAS.Repositories.Interface;
using HMAS.Services.Interface;

namespace HMAS.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepo _doctorRepo;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepo doctorRepo, IMapper mapper)
        {
            _doctorRepo = doctorRepo;
            _mapper = mapper;
        }
        public async Task<List<DoctorAvailReadDTO>> AddAvailabilityAsync(int doctorId, List<DoctorAvailDTO> availabilityDtos)
        {
            foreach (var dto in availabilityDtos)
            {
                if (dto.StartTime >= dto.EndTime)
                    throw new ArgumentException($"Start time must be earlier than end time for {dto.Day}.");
            }

            var entities = _mapper.Map<List<DoctorAvailability>>(availabilityDtos);

            await _doctorRepo.AddAvailabilityAsync(doctorId , entities);

            return _mapper.Map<List<DoctorAvailReadDTO>>(entities);
            //var duplicateDays = availabilityDtos
            //       .GroupBy(a => a.Day)
            //       .Where(g => g.Count() > 1)
            //       .Select(g => g.Key)
            //       .ToList();

            //if (duplicateDays.Any())
            //    throw new ArgumentException($"Duplicate availability found for day(s): {string.Join(", ", duplicateDays)}.");

            //// ✅ Check time ranges
            //foreach (var dto in availabilityDtos)
            //{
            //    if (dto.StartTime >= dto.EndTime)
            //        throw new ArgumentException($"Start time must be earlier than end time for {dto.Day}.");
            //}

            //var entities = _mapper.Map<List<DoctorAvailability>>(availabilityDtos);

            //await _doctorRepo.AddAvailabilityAsync(doctorId, entities);

            //return _mapper.Map<List<DoctorAvailReadDTO>>(entities);
        }

        public async Task<DoctorReadDTO> AddDoctor(DoctorDTO doctorDTO)
        {
            var dto = _mapper.Map<Doctor>(doctorDTO);
            await _doctorRepo.AddDoctor(dto);
            return _mapper.Map<DoctorReadDTO>(dto);
        }

        public async Task<LeaveReadDTO> ApplyLeave(LeaveDTO leaveDTO)
        {
            var dto = _mapper.Map<Leave>(leaveDTO);
            await _doctorRepo.ApplyLeave(dto);
            return _mapper.Map<LeaveReadDTO>(dto);
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            return await _doctorRepo.DeleteDoctor(id);
        }

        public async Task<IEnumerable<DoctorReadDTO>> GetAll()
        {
            var docs = await _doctorRepo.GetAll();
            return _mapper.Map<IEnumerable<DoctorReadDTO>>(docs);
        }

        public async Task<IEnumerable<DoctorAvailReadDTO>> GetAvailabilityAsync(int doctorId)
        {
            var entity = await _doctorRepo.GetAvailabilityAsync(doctorId);
            return _mapper.Map<IEnumerable<DoctorAvailReadDTO>>(entity);
        }

        public async Task<DoctorReadDTO?> GetById(int id)
        {
            var docs = await _doctorRepo.GetById(id);
            return docs==null ? null : _mapper.Map<DoctorReadDTO>(docs);
        }

        public async Task<DoctorReadDTO?> UpdateDoctor(int id,DoctorDTO doctorDTO)
        {
            var doc = _mapper.Map<Doctor>(doctorDTO);
            var updated = await _doctorRepo.UpdateDoctor(id,doc);
            return updated == null ? null : _mapper.Map<DoctorReadDTO>(updated);
        }
    }
}
