using HMAS.Data;
using HMAS.DTO.Doctors;
using HMAS.Models;
using HMAS.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HMAS.Repositories
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAvailabilityAsync(int doctorId, List<DoctorAvailability> availabilityDTOs)
        {
            var existing = await _context.DoctorAvailability
                  .Where(x => x.DoctorId == doctorId)
                  .ToListAsync();

            _context.DoctorAvailability.RemoveRange(existing);

            foreach (var item in availabilityDTOs)
                item.DoctorId = doctorId;

            await _context.DoctorAvailability.AddRangeAsync(availabilityDTOs);
            await _context.SaveChangesAsync();
        }

        public async Task<DoctorAvailability?> GetByDoctorAndDayAsync(int doctorId, DayOfWeek day)
        {
            return await _context.DoctorAvailability
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId && d.Day == day);
        }

        public async Task<bool> IsDoctorOnLeaveAsync(int doctorId, DateOnly date)
        {
            return await _context.Leave
                .AnyAsync(l => l.DoctorId == doctorId && date >= l.StartDate && date <= l.EndDate);
        }


        public async Task AddDoctor(Doctor doctor)
        {
            await _context.Doctor.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task ApplyLeave(Leave leave)
        {
            await _context.Leave.AddAsync(leave);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            var getDoc = await _context.Doctor
                //.Include(i=>i.Leaves)
                .FirstOrDefaultAsync(i=>i.DoctorId==id);

            if (getDoc != null)
            {
                _context.Doctor.Remove(getDoc);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _context.Doctor
                
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<DoctorAvailability>> GetAvailabilityAsync(int doctorId)
        {
            return await _context.DoctorAvailability.Where(i=>i.DoctorId==doctorId).ToListAsync();
        }

        public async Task<Doctor?> GetById(int id)
        {
            return await _context.Doctor.FindAsync(id);
        }

        public async Task<Doctor?> UpdateDoctor(int id,Doctor doctor)
        {
            var doc = await _context.Doctor.FindAsync(id);
            if(doc == null) { return null; }
            doc.Name = doctor.Name;
            doc.Specialization = doctor.Specialization;
            doc.DepartmentId = doctor.DepartmentId;

            await _context.SaveChangesAsync();
            return doc;
        }
    }
}
