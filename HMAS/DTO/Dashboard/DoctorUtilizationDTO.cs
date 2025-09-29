namespace HMAS.DTO.Dashboard
{
    public class DoctorUtilizationDTO
    {
        public string DoctorName { get; set; }
        public int TotalSlots { get; set; }
        public int BookedSlots { get; set; }
        public double UtilizationPercentage { get; set; }
    }
}
