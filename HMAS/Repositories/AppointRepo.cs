using HMAS.Data;
using HMAS.DTO.Appointments;
using HMAS.Helper;
using HMAS.Models;
using HMAS.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HMAS.Repositories
{
    public class AppointRepo : IAppointRepo
    {
        private readonly ApplicationDbContext _context;

        public AppointRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorAvailability?> GetDoctorAvailabilityAsync(int doctorId, DayOfWeek day)
        {
            return await _context.DoctorAvailability
                .FirstOrDefaultAsync(i => i.DoctorId == doctorId && i.Day == day);
        }

        public async Task<bool> IsDoctorOnLeaveAsync(int doctorId, DateOnly date)
        {
            return await _context.Leave
                .AnyAsync(i => i.DoctorId == doctorId && date >= i.StartDate && date <= i.EndDate);
        }

        public async Task<bool> IsSlotTakenAsync(int doctorId, DateOnly date, TimeOnly time)
        {
            return await _context.Appointment
                .AnyAsync(a => a.DoctorId == doctorId &&
                               a.AppointmentDate == date &&
                               a.Status == AppointmentStatus.Scheduled &&
                               time>=a.AppointmentTime &&
                               time<a.AppointmentTime.AddMinutes(30));
        }

        public async Task AddApp(Appointment appointment)
        {
            await _context.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppo()
        {
           return await _context.Appointment
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppoByKey(string keyword)
        {
            var query = _context.Appointment.AsNoTracking();

            if (int.TryParse(keyword, out int doctorId))
            {
                return await query
                    .Where(i => i.DoctorId == doctorId)
                    .ToListAsync();
            }
            else if (DateOnly.TryParse(keyword, out var date))
            {
                return await query
                    .Where(i => i.AppointmentDate == date)
                    .ToListAsync();
            }

            return new List<Appointment>();
        }

        public async Task<Appointment?> UpdateAppo(int id, Appointment appointment)
        {
            var appo = await _context.Appointment.FindAsync(id);
            appo.AppointmentDate = appointment.AppointmentDate;
            appo.AppointmentTime = appointment.AppointmentTime;
            appo.PatientId = appointment.PatientId;
            appo.DoctorId=appointment.DoctorId;
            appo.Status = appointment.Status;
            await _context.SaveChangesAsync();
            return appo;
        }

        public async Task<Appointment?> GetAppoById(int id)
        {
            return await _context.Appointment.FindAsync(id);
        }


        //public async Task<bool> AddApp(Appointment appointment)
        //{
        //    if (await IsSlotAvailable(appointment.DoctorId, appointment.AppointmentDate, appointment.AppointmentTime))
        //    {
        //        await _context.AddAsync(appointment);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<bool> IsSlotAvailable(int docId, DateOnly date, TimeOnly time)
        //{
        //    var day = date.DayOfWeek;
        //    var doc = await _context.DoctorAvailability
        //        .FirstOrDefaultAsync(i => i.DoctorId == docId);
        //    if (doc == null || time < doc.StartTime || time >= doc.EndTime)
        //    {
        //        return false;
        //    }

        //    var leave = await _context.Leave
        //        .AnyAsync(i => i.DoctorId == docId && date >= i.StartDate && date <= i.EndDate);
        //    if (leave)
        //    {
        //        return false;
        //    }

        //    var slot = await _context.Appointment
        //        .AnyAsync(a => a.DoctorId == docId
        //                    && a.AppointmentDate == date
        //                    && a.AppointmentTime == time
        //                    && a.status == "Scheduled");


        //    if (slot)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public bool CanBookAppointment(int id, DateOnly date, TimeOnly time)
        //{
        //    var isAvailable = _context.DoctorAvailability.Any(a =>
        //        a.Day == date.DayOfWeek &&
        //        time >= a.StartTime &&
        //        time <= a.EndTime);

        //    var isOnLeave = _context.Leave.Any(l =>
        //        date >= l.StartDate && date <= l.EndDate);

        //    var hasConflict = _context.Appointment.Any(a =>
        //     a.DoctorId == id &&
        //        a.AppointmentDate == date &&
        //        a.AppointmentTime == time &&
        //        a.status == "Scheduled");

        //    return isAvailable && !isOnLeave && !hasConflict;
        //}

        //public async Task<bool> IsSlotTaken(int doctorId, DateOnly date, TimeOnly time)
        //{
        //    return await _context.Appointment
        //        .AnyAsync(a => a.DoctorId == doctorId
        //                    && a.AppointmentDate == date
        //                    && a.AppointmentTime == time
        //                    && a.status == "Scheduled");
        //}

        //public async Task<bool> IsSlotAvailable(int doctorId, DateOnly date, TimeOnly time)
        //{
        //    var day = date.DayOfWeek;

        //    var availability = await _context.DoctorAvailability
        //        .FirstOrDefaultAsync(a => a.DoctorId == doctorId && a.Day == day);
        //    if (availability == null || time < availability.StartTime || time >= availability.EndTime)
        //        return false;

        //    var onLeave = await _context.Leave
        //        .AnyAsync(l => l.DoctorId == doctorId && date >= l.StartDate && date <= l.EndDate);
        //    if (onLeave)
        //        return false;

        //    var taken = await _context.Appointment
        //        .AnyAsync(a => a.DoctorId == doctorId && a.AppointmentDate == date && a.AppointmentTime == time && a.status == "Scheduled");
        //    if (taken)
        //        return false;

        //    return true;
        //}


        ////public async Task<bool> IsSlotAvailable(int doctorId, DateOnly date, TimeOnly time)
        ////{
        ////    var day = date.DayOfWeek;

        ////    var availability = await _doctorRepo.GetByDoctorAndDayAsync(doctorId, day);
        ////    if (availability == null || time < availability.StartTime || time >= availability.EndTime)
        ////        return false;

        ////    var onLeave = await _doctorRepo.IsDoctorOnLeaveAsync(doctorId, date);
        ////    if (onLeave)
        ////        return false;

        ////    var taken = await IsSlotTaken(doctorId, date, time);
        ////    if (taken)
        ////        return false;

        ////    return true;
        ////}
    }
}
