using HMAS.DTO.Dashboard;
using HMAS.Helper;
using HMAS.Models;
using Microsoft.EntityFrameworkCore;

namespace HMAS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<Records> MedicalRecord { get; set; }



        //It helps in only defining the above DbSet as virtual table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyAppoDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<DoctorUtilizationDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<PatientFrequencyDTO>().HasNoKey().ToView(null);

            // Patient → MedicalRecord: No cascade delete
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.MedicalRecords)
                .WithOne(r => r.Patient)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment → MedicalRecord: Also disable cascade delete
            //modelBuilder.Entity<Appointment>()
                
            //    .WithOne(r => r.Appointment)
            //    .HasForeignKey(r => r.AppointmentId)
            //    .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction
        }
    }
}
