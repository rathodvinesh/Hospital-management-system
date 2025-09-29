using HMAS.Data;
using HMAS.Models;
using HMAS.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HMAS.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _context.User.FirstOrDefaultAsync(i=>i.Username == userName);
            if (user == null) return null;
            return user;
        }
    }
}
