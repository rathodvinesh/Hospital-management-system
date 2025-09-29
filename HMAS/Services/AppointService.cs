using AutoMapper;
using HMAS.DTO.Appointments;
using HMAS.Models;
using HMAS.Repositories.Interface;
using HMAS.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using System.IO;

namespace HMAS.Services
{
    public class AppointService : IAppointService
    {
        private readonly IAppointRepo _appointRepo;
        private readonly IPatientRepo _patRepo;
        private readonly IDoctorRepo _docRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _email;

        public AppointService(IAppointRepo appointRepo, IMapper mapper, IEmailService email,IPatientRepo patientRepo,IDoctorRepo docRepo)
        {
            _appointRepo = appointRepo;
            _mapper = mapper;
            _email = email;
            _patRepo = patientRepo;
            _docRepo = docRepo;
        }

        public async Task<AppointmentResult?> AddAppointment(AppointmentDTO dto)
        {
            var day = dto.AppointmentDate.DayOfWeek;

            if (dto.AppointmentTime >= new TimeOnly(13, 0) && dto.AppointmentTime < new TimeOnly(14, 0))
            {
                return new AppointmentResult
                {
                    Success = false,
                    Message = "Appointments cannot be booked during the doctor's break time (1 PM - 2 PM)."
                };
            }

            var availability = await _appointRepo.GetDoctorAvailabilityAsync(dto.DoctorId, day);
            if (availability == null)
            {
                return new AppointmentResult { Success = false, Message = "Doctor is not available on this day." };
            }

            if (dto.AppointmentTime < availability.StartTime || dto.AppointmentTime >= availability.EndTime)
            {
                return new AppointmentResult { Success = false, Message = "Doctor is not available at this time." };
            }

            var isOnLeave = await _appointRepo.IsDoctorOnLeaveAsync(dto.DoctorId, dto.AppointmentDate);
            if (isOnLeave)
            {
                return new AppointmentResult { Success = false, Message = "Doctor is on leave on this date." };
            }

            var isTaken = await _appointRepo.IsSlotTakenAsync(dto.DoctorId, dto.AppointmentDate, dto.AppointmentTime);
            if (isTaken)
            {
                return new AppointmentResult { Success = false, Message = "This slot is already booked." };
            }

            var appointment = _mapper.Map<Appointment>(dto);
            await _appointRepo.AddApp(appointment);

            if (!await EmailBody(dto))
            {
                 return new AppointmentResult { Success = false, Message = "Issue in email" };
            }

            var readDto = _mapper.Map<AppointmentReadDTO>(appointment);
            return new AppointmentResult
            {
                Success = true,
                Message = "Appointment booked successfully.",
                Appointment = readDto
            };
        }

        public async Task<IEnumerable<AppointmentReadDTO>> GetAppo()
        {
            var appo = await _appointRepo.GetAppo();
            return _mapper.Map<IEnumerable<AppointmentReadDTO>>(appo);
        }

        public Task<Appointment?> GetAppoById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppointmentReadDTO>> GetAppoByKey(string keyword)
        {
            var appo = await _appointRepo.GetAppoByKey(keyword);
            return _mapper.Map<IEnumerable<AppointmentReadDTO>>(appo);

        }

        public async Task<AppointmentReadDTO> UpdateAppo(int id, AppointmentDTO appointmentReadDTO)
        {
            var prev = await _appointRepo.GetAppoById(id);

            var oppo = await _appointRepo.UpdateAppo(id, _mapper.Map<Appointment>(appointmentReadDTO));
            if (oppo == null || prev==null)
            {
                return null;
            }
            if (appointmentReadDTO.Status!=prev.Status || await EmailBody(appointmentReadDTO))
            {
                return _mapper.Map<AppointmentReadDTO>(oppo);
            }
             return null;
        }

        public async Task<bool> EmailBody(AppointmentDTO dto)
        {
            var pat = await _patRepo.GetById(dto.PatientId);
            var doc = await _docRepo.GetById(dto.DoctorId);

            if (pat != null && doc != null)
            {
                string subject = $"Appointment Confirmation - HMAS";
                string body = $@"
                <p>Hello {pat.PatientName},</p>
                <p>Your appointment with Dr. {doc.Name} is {dto.Status} for <b>{dto.AppointmentDate}</b> at <b>{dto.AppointmentTime}</b>.</p>
                <p><b>Appointment Status : {dto.Status}</b></p>
                <br/>
                <p>Thanks,<br/>HMAS</p>";

                await _email.SendEmailAsync(pat.Email, subject, body);
                return true;
            }
            return false;
        }

        //public async Task<AppointmentReadDTO> AddAppointment(AppointmentDTO appointmentDTO)
        //{
        //    var appointment = _mapper.Map<Appointment>(appointmentDTO);

        //    await _appointRepo.AddApp(appointment);

        //    //if (!isSaved)
        //    //    return null;

        //    return _mapper.Map<AppointmentReadDTO>(appointment);
        //}

    }
}
