using Primrose.Models;
using Supabase;

namespace Primrose.Repositories.User;

public class UserRepository(Client client) : IUserRepository
{
    private readonly Client _client = client;

    public async Task<bool> UpdateUser(PrimroseUser user)
    {
        var result = await _client.From<PrimroseUser>()
            .Update(user);

        return result.Model != null;
    }

    public async Task<bool> CreateUser(string email, string name, string passwordHash)
    {
        var user = new PrimroseUser
        {
            Email = email,
            Name = name,
            PasswordHash = passwordHash
        };

        var result = await _client.From<PrimroseUser>()
            .Insert(user);

        return result.Model != null;
    }

    public async Task<PrimroseUser?> GetUser(string email)
    {
        var result = await _client.From<PrimroseUser>()
            .Where(x => x.Email == email)
            .Get();

        return result.Model;
    }
}