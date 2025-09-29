using HMAS.DTO.Auth;
using HMAS.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HMAS.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO regiterDTO)
        {
            var result = await _service.Register(regiterDTO);
            return Ok(new {status = "Success", message=result});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var token = await _service.Login(loginDTO);
            if(token == null)
            {
                return Unauthorized(new { status = "error", message = "invalid Credential" });
            }
            return Ok(new {status="Success",message = token});
        }
    }
}
