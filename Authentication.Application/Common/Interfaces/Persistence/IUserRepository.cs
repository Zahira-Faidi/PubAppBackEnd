using Authentication.Domain.Entities;

namespace Authentication.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User> GetByIdAsync(string userId);
    Task<User> GetUserByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task AssignUserRoleAsync(string userId, string role);
}