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

    // ADD USER
    public async Task AddAsync(User user)
    {
        await _collection.InsertOneAsync(user);
    }

    // DELETE USER
    public async Task<bool> DeleteUserAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(u => u.Id == id);
        return result.DeletedCount > 0;
    }

    // GET ALL USERS
    public async Task<List<User>> GetUsersAsync()
    {
        return await _collection.Find(u => true).ToListAsync();
    }

    // GET USER BY EMAIL
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
        return user;
    }

    // GET USER BY ID
    public async Task<User> GetByIdAsync(string userId)
    {
        var user = await _collection.Find(u => u.Id == userId).FirstOrDefaultAsync();
        return user;
    }

    // UPDATE USER
    public async Task<bool> UpdateAsync(User user)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
        var res = await _collection.ReplaceOneAsync(filter, user);
        return res.ModifiedCount > 0;
    }

    // ASSIGN USER ROLE
    public async Task AssignUserRoleAsync(string userId, string role)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var update = Builders<User>.Update.Set(u => u.Role, role);
        await _collection.UpdateOneAsync(filter, update);
    }
}