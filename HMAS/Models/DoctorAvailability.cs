namespace HMAS.Models
{
    public class DoctorAvailability
    {
        public int DoctorAvailabilityId { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public Doctor Doctor { get; set; }
    }
}
