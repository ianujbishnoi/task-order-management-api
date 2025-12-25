using Microsoft.AspNetCore.Mvc;
using TaskOrderManagement.Application.DTOs;
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
        public async Task<IActionResult> Create(CreateUserRequestDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password, // hashing DAY 6
                Role = dto.Role
            };

            var created = await _userService.CreateUserAsync(user);

            var response = new UserResponseDto
            {
                Id = created.Id,
                FullName = created.FullName,
                Email = created.Email
            };

            return CreatedAtAction(nameof(Create), response);
        }
    }
}
