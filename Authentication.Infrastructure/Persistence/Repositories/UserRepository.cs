using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Domain.Entities;
using MongoDB.Driver;

namespace Authentication.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<User>("Users");
    }
    public async Task<User> GetByIdAsync(string userId)
    {
        var user = await _collection.Find(u => u.Id == userId).FirstOrDefaultAsync();
        return user;
    }
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
        return user;
    }
    public async Task AddAsync(User user)
    {
        await _collection.InsertOneAsync(user);
    }
    public async Task UpdateAsync(User user)
    {
        await _collection.ReplaceOneAsync(u => u.Id == user.Id, user);
    }

    public async Task AssignUserRoleAsync(string userId, string role)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var update = Builders<User>.Update.Set(u => u.Role, role);
        await _collection.UpdateOneAsync(filter, update);
    }
}
