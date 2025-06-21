using Primrose.API.Models.Authentication;

namespace Primrose.API.Models.Login;

public sealed class LoginResponse(UserAuthenticationResult result) 
    : IResponse
{
    public UserAuthenticationResult Result { get; set; } = result;
}