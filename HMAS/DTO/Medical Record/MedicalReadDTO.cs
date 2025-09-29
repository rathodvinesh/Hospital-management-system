namespace HMAS.DTO.Medical_Record
{
    public class MedicalReadDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
