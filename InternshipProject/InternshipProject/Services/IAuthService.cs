using InternshipProject.Models;

namespace InternshipProject.Services.abstracts
{
    public interface IAuthService
    {
        Task<string?> Register(RegisterRequestModel model);
        Task<string?> Login(LoginRequestModel model);
        Task<bool> ResetPassword(ResetPasswordRequestModel model);

    }
}
