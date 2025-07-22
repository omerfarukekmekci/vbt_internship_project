using InternshipProject.Models;

namespace InternshipProject.Services.abstracts
{
    public interface IAuthService
    {
        Task<string?> Register(RegisterRequestModel model); 
        Task<string?> Login(string email, string password);
    }
}
