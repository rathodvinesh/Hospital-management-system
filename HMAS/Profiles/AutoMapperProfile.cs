using AutoMapper;
using HMAS.DTO.Appointments;
using HMAS.DTO.Departments;
using HMAS.DTO.Doctors;
using HMAS.DTO.Leaves;
using HMAS.DTO.Medical_Record;
using HMAS.DTO.Patient;
using HMAS.Models;

namespace HMAS.Profiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //Patient
            CreateMap<PatientDTO,Patient>();
            CreateMap<Patient, PatientReadDTO>();
            CreateMap<Patient, PatientReadAppointDTO>();

            //Doctor
            CreateMap<DoctorDTO, Doctor>();
            CreateMap<Doctor, DoctorReadDTO>();

            //Department
            CreateMap<DepartmentDTO, Department>();
            CreateMap<Department, DepartmentReadDTO>();

            //Appointment
            CreateMap<AppointmentDTO,Appointment>();
            CreateMap<Appointment,AppointmentReadDTO>();

            //Leave
            CreateMap<LeaveDTO, Leave>();
            CreateMap<Leave,LeaveReadDTO>();

            //Add availability
            CreateMap<DoctorAvailDTO, DoctorAvailability>();
            CreateMap<DoctorAvailability, DoctorAvailReadDTO>()
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToString()));

            //Appointment
            CreateMap<AppointmentDTO,Appointment>();
            //CreateMap<Appointment, AppointmentReadDTO>();
            CreateMap<Appointment, AppointmentReadDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            //Medical Record
            CreateMap<MedicalDTO, Records>();
            CreateMap<Records, MedicalReadDTO>();
        }
    }
}
