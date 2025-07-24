using InternshipProject.Models;
using InternshipProject.Services.abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InternshipProject.Data;

namespace InternshipProject.Services.concretes
{
    public class AuthService : IAuthService
    {
        private readonly InternPortalContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(InternPortalContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> Register(RegisterRequestModel model)
        {
            if (await _context.Users.AnyAsync(x => x.Email == model.Email))
                return null;

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.Password,
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return GenerateJwtToken(user);
        }

        public async Task<string?> Login(LoginRequestModel model)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == model.Email && x.PasswordHash == model.Password);

            if (user == null)
                return null;

            return GenerateJwtToken(user);
        }

        public async Task<bool> ResetPassword(ResetPasswordRequestModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
                return false;

            user.PasswordHash = model.NewPassword;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }


        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? "default_key";
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "default_issuer";
            var jwtAudience = _configuration["Jwt:Audience"] ?? "default_audience";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
