using Primrose.Models.Authentication;

namespace Primrose.Repositories;


public interface IUserRepository
{
    public Task<bool> CreateUser(string email, string username, string passwordHash);
    public Task<User?> GetUser(string email);
    public Task<bool> UpdateUser(User user);
}