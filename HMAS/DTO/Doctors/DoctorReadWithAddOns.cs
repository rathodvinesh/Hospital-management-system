using HMAS.Models;

namespace HMAS.DTO.Doctors
{
    public class DoctorReadWithAddOns
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public int DepartmentId { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<DoctorAvailability> DoctorAvailabilities { get; set; }
    }
}
