namespace HMAS.DTO.Medical_Record
{
    public class MedicalDTO
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
    }
}
