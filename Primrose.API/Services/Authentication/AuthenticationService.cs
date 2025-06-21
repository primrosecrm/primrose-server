using Primrose.API.Models.Authentication;

namespace Primrose.API.Services.Authentication;

public class AuthenticationService
    : IAuthenticationService<UserCredential, UserAuthenticationResult>
{
    public UserAuthenticationResult Authenticate(UserCredential credentials)
    {
        var username = credentials.Username;
        var password = credentials.Password;

        return UserAuthenticationResult.Ok();
    }
}