namespace HMAS.Models
{
    public class Leave
    {
        public int LeaveId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Reason { get; set; }

        public Doctor Doctor { get; set; }
    }
}
