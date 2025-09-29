using HMAS.Models;

namespace HMAS.DTO.Departments
{
    public class DepartmentDTOWithDoc
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
