using TaskOrderManagement.Domain.Entities;

namespace TaskOrderManagement.Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}
