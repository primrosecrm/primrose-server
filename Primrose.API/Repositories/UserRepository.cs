using Primrose.API.Models.Authentication;
using Supabase;

namespace Primrose.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly Client _client;

    public UserRepository(Client client)
    {
        _client = client;
    }

    public async Task<bool> UpdateUser(User user)
    {
        var result = await _client.From<User>()
            .Update(user);

        return result.Model != null;
    }

    public async Task<bool> CreateUser(string email, string name, string passwordHash)
    {
        var user = new User
        {
            Email = email,
            Name = name,
            PasswordHash = passwordHash
        };

        var result = await _client.From<User>()
            .Insert(user);

        return result.Model != null;
    }

    public async Task<User?> GetUser(string email)
    {
        var result = await _client.From<User>()
            .Where(x => x.Email == email)
            .Get();

        return result.Model;
    }
}