using Microsoft.AspNetCore.Mvc;
using TaskOrderManagement.Application.Interfaces;
using TaskOrderManagement.Domain.Entities;
using TaskOrderManagement.Domain.Enum;

namespace TaskOrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var user = new User
            {
                FullName = "Test User",
                Email = "test@example.com",
                PasswordHash = "hashed-password",
                Role = UserRole.User
            };

            var result = await _userService.CreateUserAsync(user);
            return Ok(result);
        }
    }
}
