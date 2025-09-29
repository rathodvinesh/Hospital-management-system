using HMAS.Data;
using HMAS.Models;
using HMAS.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HMAS.Repositories
{
    public class MedicalRepo : IMedicalRepo
    {
        private readonly ApplicationDbContext _context;

        public MedicalRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddRecord(Records medicalRecord)
        {
            medicalRecord.CreatedAt= DateTime.Now;
            await _context.MedicalRecord.AddAsync(medicalRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Records>> GetAllRecords()
        {
            return await _context.MedicalRecord
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Records?> GetRecord(int id)
        {
            return await _context.MedicalRecord.FindAsync(id);
        }
    }
}
