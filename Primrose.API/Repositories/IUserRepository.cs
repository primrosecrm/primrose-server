using Primrose.API.Models.Authentication;

namespace Primrose.API.Repositories;


public interface IUserRepository
{
    public Task<bool> CreateUser(string email, string username, string passwordHash);
    public Task<User?> GetUser(string email);
}