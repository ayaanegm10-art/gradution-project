using try4.Models;

namespace try4.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        DateTime GetExpirationTime();
    }
}
