using Primrose.Entities.DeactivateUser;
using Primrose.Entities.LoginUser;
using Primrose.Entities.RegisterUser;

namespace Primrose.Services.Authentication;

public interface IAuthenticationService
{
    Task<LoginUserResponse> LoginUser(LoginUserRequest request);
    Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
    Task<DeactivateUserResponse> DeactivateUser(DeactivateUserRequest request);
}