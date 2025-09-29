namespace HMAS.DTO.Doctors
{
    public class DoctorReadDTO
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public int DepartmentId { get; set; }
        //public bool IsOnLeave { get; set; }
    }
}
