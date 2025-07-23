using Primrose.Entities.LoginUser;
using Primrose.Entities.RegisterUser;

namespace Primrose.Services.Authentication;

public interface IAuthService
{
    Task<LoginUserResponse> LoginUser(LoginUserRequest request);
    Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
}