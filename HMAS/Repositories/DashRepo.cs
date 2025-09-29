using HMAS.Data;
using HMAS.DTO.Dashboard;
using HMAS.Helper;
using HMAS.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HMAS.Repositories
{
    public class DashRepo : IDashRepo
    {
        private readonly ApplicationDbContext _context;

        public DashRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DailyAppoDTO>> GetDailyAppointmentsByDoctor()
        {
            var sql = @"
                    SELECT 
                     FORMAT(AppointmentDate, 'yyyy-MM-dd') AS Date,
                    d.Name AS DoctorName,
                    dept.DepartmentName,
                    COUNT(*) AS Count
                FROM Appointment a
                JOIN Doctor d ON a.DoctorId = d.DoctorId
                JOIN Department dept ON d.DepartmentId = dept.DepartmentId
                GROUP BY FORMAT(AppointmentDate, 'yyyy-MM-dd'), d.Name, dept.DepartmentName  
                ";

            return await _context.Set<DailyAppoDTO>().FromSqlRaw(sql).ToListAsync();
        }

        //public async Task<IEnumerable<DoctorUtilizationDTO>> GetDoctorUtilization()
        //{
            
        //}

        public async Task<IEnumerable<PatientFrequencyDTO>> GetPatientVisitFrequency()
        {
            var sql = @"
                     SELECT 
                    p.PatientName,
                    COUNT(*) AS VisitCount
                FROM Appointment a
                INNER JOIN Patient p ON a.PatientId = p.PatientId
                GROUP BY p.PatientName
                ORDER BY VisitCount DESC
                ";

            return await _context.Set<PatientFrequencyDTO>().FromSqlRaw(sql).ToListAsync();
        }

        //    public async Task<DashDTO> GetDashboardData()
        //    {
        //        var today = DateOnly;
        //        var last7Days = DateOnly.FromDateTime(DateTime.Today.AddDays(-6));

        //        var totalDoctors = await _context.Doctor.CountAsync();
        //        var totalPatients = await _context.Patient.CountAsync();
        //        var todaysAppointments = await _context.Appointment.CountAsync(a => a.AppointmentDate == today);
        //        var pendingAppointments = await _context.Appointment.CountAsync(a => a.Status == AppointmentStatus.Scheduled);

        //        var weeklyAppointments = await _context.Appointment
        //            .Where(a => a.AppointmentDate >= last7Days)
        //            .GroupBy(a => a.AppointmentDate)
        //            .Select(g => new WeekChartDTO
        //            {
        //                Date = g.Key.ToString("yyyy-MM-dd"),
        //                Count = g.Count()
        //            }).ToListAsync();

        //        var departmentWiseAppointments = await _context.Appointment
        //            .Include(a => a.Doctor)
        //            .ThenInclude(d => d.Department)
        //            .GroupBy(a => a.Doctor.Department.DepartmentName)
        //            .Select(g => new DeptChartDTO
        //            {
        //                Department = g.Key,
        //                Count = g.Count()
        //            }).ToListAsync();

        //        return new DashDTO
        //        {
        //            TotalDoctors = totalDoctors,
        //            TotalPatients = totalPatients,
        //            TodaysAppointments = todaysAppointments,
        //            PendingAppointments = pendingAppointments,
        //            WeeklyAppointments = weeklyAppointments,
        //            DepartmentWiseAppointments = departmentWiseAppointments
        //        };
        //    //}
    }
}
