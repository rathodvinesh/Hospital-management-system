namespace HMAS.DTO.Leaves
{
    public class LeaveReadDTO
    {
        public int LeaveId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Reason { get; set; }
    }
}
