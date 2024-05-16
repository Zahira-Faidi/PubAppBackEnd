using Authentication.Domain.Entities;

namespace Authentication.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User> GetByIdAsync(string userId);
    Task<User> GetUserByEmailAsync(string email);
    Task AddAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task AssignUserRoleAsync(string userId, string role);
    Task<List<User>> GetUsersAsync();
    Task<bool> DeleteUserAsync(string id);
}