using Microsoft.AspNetCore.Identity;
using try4.Models;
using try4.Services.Interfaces;

namespace try4.Services.Implementation
{
    public class AuthService(
        UserManager<User> userManager,
        IJwtService jwtService,
        ILogger<AuthService> logger) : IAuthService
    {
        public async Task<AuthResponse?> AuthenticateAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                logger.LogWarning("Authentication failed for email: {Email}", email);
                return null;
            }

            var validPassword = await userManager.CheckPasswordAsync(user, password);
            if (!validPassword)
            {
                logger.LogWarning("Authentication failed for email: {Email}", email);
                return null;
            }

            var role = Enum.IsDefined(user.Role) ? user.Role : UserRole.User;
            return new AuthResponse
            {
                AccessToken = jwtService.GenerateToken(user),
                ExpiresAt = jwtService.GetExpirationTime(),
                User = new
                {
                    user.Id,
                    user.Email,
                    Role = role.ToString()
                }
            };
        }
    }
}
