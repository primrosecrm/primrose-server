namespace Primrose.Entities.LoginUser;

public sealed class LoginUserRequest
    : ApiRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}