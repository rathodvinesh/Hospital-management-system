using HMAS.Data;
using HMAS.Models;
using HMAS.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HMAS.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly ApplicationDbContext _context;

        public PatientRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPatient(Patient patient)
        {
            await _context.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
             return await _context.Patient.AsNoTracking().ToListAsync();
        }

        public async Task<Patient?> GetById(int id)
        {
            return await _context.Patient
                .Include(i => i.Appointments)
                .Include(i=>i.MedicalRecords)
                .FirstOrDefaultAsync(i => i.PatientId == id);
        }

        public async Task<IEnumerable<Patient>> Search(string keyword)
        {
            var pat = await _context.Patient
                .Where(i =>
                    i.PatientName.Contains(keyword) ||
                    i.Email.Contains(keyword) ||
                    i.PhoneNumber.Contains(keyword)
                ).ToListAsync();
            return pat;
        }
    }
}
