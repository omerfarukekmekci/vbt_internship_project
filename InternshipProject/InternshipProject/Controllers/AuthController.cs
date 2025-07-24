using InternshipProject.Models;
using InternshipProject.Services.abstracts;
using Microsoft.AspNetCore.Mvc;

namespace InternshipProject.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
        {
            var token = await _authService.Register(model);
            return token == null ? BadRequest("Kayıt başarısız.") : Ok(new { token });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            var token = await _authService.Login(email, password);
            return token == null ? Unauthorized("Geçersiz bilgiler.") : Ok(new { token });
        }
    }
}
