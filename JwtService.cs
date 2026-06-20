using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using try4.Models;
using try4.Services.Interfaces;

namespace try4.Services.Implementation
{
    public sealed class JwtService(IConfiguration config) : IJwtService
    {
        public string GenerateToken(User user)
        {
            var issuer = config["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Jwt:Issuer is missing from configuration.");
            var audience = config["Jwt:Audience"]
                ?? throw new InvalidOperationException("Jwt:Audience is missing from configuration.");
            var jwtKey = config["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key is missing from configuration.");

            var role = Enum.IsDefined(user.Role) ? user.Role : UserRole.User;
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName ?? string.Empty),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.Role, role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: GetExpirationTime(),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime GetExpirationTime()
        {
            var expiresInMinutes = int.TryParse(config["Jwt:ExpiresInMinutes"], out var minutes)
                ? minutes
                : 60;

            return DateTime.UtcNow.AddMinutes(expiresInMinutes);
        }
    }
}
