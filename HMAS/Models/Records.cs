using System.Text.Json.Serialization;

namespace HMAS.Models
{
    public class Records
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Appointment Appointment { get; set; }
        [JsonIgnore]
        public Patient Patient { get; set; }
    }
}
