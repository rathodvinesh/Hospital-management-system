using HMAS.Models;

namespace HMAS.Repositories.Interface
{
    public interface IUserRepo
    {
        Task<User> GetUserByUserName(string userName);
        Task AddUser(User user);
    }
}
