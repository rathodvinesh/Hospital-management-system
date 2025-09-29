namespace HMAS.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        //public bool IsOnLeave { get; set; } = false;
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<DoctorAvailability> DoctorAvailabilities { get; set; }
    }
}
