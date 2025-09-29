using HMAS.Helper;
using System.Text.Json.Serialization;

namespace HMAS.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public AppointmentStatus Status { get; set; }

        [JsonIgnore]
        public Patient Patient { get; set; }
        //[JsonIgnore]
        public Doctor Doctor { get; set; }
    }
}
