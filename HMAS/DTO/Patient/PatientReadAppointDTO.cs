using HMAS.DTO.Appointments;
using HMAS.Models;
using System.Text.Json.Serialization;

namespace HMAS.DTO.Patient
{
    public class PatientReadAppointDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DOB { get; set; }
        public string Gender { get; set; }

        //[JsonIgnore]
        public List<AppointmentReadDTO>? Appointments { get; set; }
        //[JsonIgnore]
        public List<Records>? MedicalRecords { get; set; }
    }
}
