using HMAS.Helper;

namespace HMAS.DTO.Appointments
{
    public class AppointmentReadDTO
    {
        public int AppointmentId { get; set; }
        //public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string Status { get; set; }
    }
}
