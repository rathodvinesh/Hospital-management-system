﻿namespace HMAS.DTO.Leaves
{
    public class LeaveDTO
    {
        public int DoctorId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Reason { get; set; }
    }
}
