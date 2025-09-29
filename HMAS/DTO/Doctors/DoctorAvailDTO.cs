using System.Text.Json.Serialization;

namespace HMAS.DTO.Doctors
{
    public class DoctorAvailDTO
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DayOfWeek Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }

}
