using System.Text.Json.Serialization;

namespace HMAS.DTO.Doctors
{
    public class DoctorAvailReadDTO
    {
        public int DoctorAvailabilityId { get; set; }
        public int DoctorId { get; set; }
        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public string Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
