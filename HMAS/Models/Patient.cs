namespace HMAS.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DOB { get; set; }
        public string Gender { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        //Medical records also in phase 2
        public ICollection<Records> MedicalRecords { get; set; }
    }
}
