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
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var token = await _authService.Login(model);
            if (token == null)
                return Unauthorized("Geçersiz e-posta ya da şifre.");

            return Ok(new { token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestModel model)
        {
            var result = await _authService.ResetPassword(model);
            if (!result)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok("Şifre güncellendi.");
        }


        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(" JWT ile erişim sağlandı.");
        }
    }
}
