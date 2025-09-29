using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HMAS.DTO.Auth;
using HMAS.Helper;
using HMAS.Models;
using HMAS.Repositories.Interface;
using HMAS.Services.Interface;
using Microsoft.IdentityModel.Tokens;

namespace HMAS.Services
{
    public class AuthServiceImple : IAuthService
    {
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _config;

        public AuthServiceImple(IUserRepo userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            var user = await _userRepo.GetUserByUserName(loginDTO.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.PasswordHash, user.PasswordHash)){
                return "Invalid Password";
            }
            return GenerateJwt(user);
        }

        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtConfig:Issuer"],
                audience: _config["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Register(RegisterDTO registerDTO)
        {
            if(await _userRepo.GetUserByUserName(registerDTO.Username) != null)
            {
                return "User already registered";
            }

            var register = new User
            {
                Username = registerDTO.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.PasswordHash),
                Role = registerDTO.Role,
            };
            await _userRepo.AddUser(register);
            return "User registered successfully";
        }
    }
}
