using try4.Models;

namespace try4.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> AuthenticateAsync(string email, string password);
    }
}
