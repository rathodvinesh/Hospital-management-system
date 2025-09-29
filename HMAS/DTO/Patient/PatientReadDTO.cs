namespace HMAS.DTO.Patient
{
    public class PatientReadDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DOB { get; set; }
        public string Gender { get; set; }
    }
}
