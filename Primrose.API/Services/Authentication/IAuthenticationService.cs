using Primrose.API.Entities.Login;
using Primrose.API.Entities.RegisterUser;

namespace Primrose.API.Services.Authentication;

public interface IAuthenticationService
{
    Task<LoginUserResponse> LoginUser(LoginUserRequest request);
    Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
}
