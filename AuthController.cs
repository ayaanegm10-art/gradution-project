using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using try4.Models;
using try4.Services.Interfaces;

namespace try4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public AuthController(
            UserManager<User> userManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public class RegisterDto
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
            public required string FullName { get; set; }
        }

        public class LoginDto
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                UserFullName = dto.FullName,
                EmailConfirmed = true,
                Role = UserRole.User
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { user.Id, user.Email, user.Role });
        }
        //ExternalLoginInfo ExternalLogin { get; set; }
        //   ==============================================================
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _authService.AuthenticateAsync(dto.Email, dto.Password);
            if (response == null) return Unauthorized("Invalid email or password.");

            return Ok(new
            {
                AccessToken = response.AccessToken,
                expiresAt = response.ExpiresAt,
                user = response.User
            });
        }
    }
}
