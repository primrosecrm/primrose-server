using Primrose.Models.Authentication;

namespace Primrose.Repositories.User;

public interface IUserRepository
{
    public Task<bool> CreateUser(string email, string username, string passwordHash);
    public Task<PrimroseUser?> GetUser(string email);
    public Task<bool> UpdateUser(PrimroseUser user);
}