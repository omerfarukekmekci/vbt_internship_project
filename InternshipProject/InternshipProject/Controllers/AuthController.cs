using InternshipProject.Models;
using InternshipProject.Services.abstracts;
using Microsoft.AspNetCore.Authorization;
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
            if (token == null)
                return BadRequest("Kayıt başarısız.");

            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            var token = await _authService.Login(email, password);
            if (token == null)
                return Unauthorized("Geçersiz e-posta ya da şifre.");

            return Ok(new { token });
        }

        
    }
}
