using InternshipProject.Data;
using InternshipProject.Models;
using InternshipProject.Services.abstracts;
using Microsoft.EntityFrameworkCore;

namespace InternshipProject.Services.concretes
{
    public class AuthService : IAuthService
    {
        private readonly InternPortalContext _context;

        public AuthService(InternPortalContext context)
        {
            _context = context;
        }

        public async Task<string?> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.Password,
                Role = model.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return GenerateSimpleToken(user);
        }


        public async Task<string?> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
            if (user == null) return null;

            return GenerateSimpleToken(user);
        }

        private string GenerateSimpleToken(User user)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{user.Email}:{DateTime.UtcNow.Ticks}"));
        }
    }
}
