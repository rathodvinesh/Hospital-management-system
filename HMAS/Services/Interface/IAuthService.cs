using HMAS.DTO.Auth;

namespace HMAS.Services.Interface
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDTO registerDTO);
        Task<string> Login(LoginDTO loginDTO);
    }
}
