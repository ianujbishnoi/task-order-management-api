using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskOrderManagement.Application.DTOs;
using TaskOrderManagement.Application.Interfaces;
using TaskOrderManagement.Infrastructure.Security;

namespace TaskOrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenGenerator _jwt;

        public AuthController(IUserService userService, JwtTokenGenerator jwt)
        {
            _userService = userService;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            if (!PasswordHasher.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user);
            return Ok(new { token });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("secure")]
        public IActionResult SecureEndpoint()
        {
            return Ok("Only admin can access");
        }
    }
}
