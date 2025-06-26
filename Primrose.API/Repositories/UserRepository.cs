using Microsoft.EntityFrameworkCore;
using Primrose.API.Context;
using Primrose.API.Models.Authentication;

namespace Primrose.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PrimroseContext _db;

    public UserRepository(PrimroseContext db)
    {
        _db = db;
    }

    public async Task<bool> CreateUser(string email, string name, string passwordHash)
    {
        var user = new User
        {
            Email = email,
            Name = name,
            PasswordHash = passwordHash
        };

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<User?> GetUser(string email)
    {
        return await _db.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}