namespace Primrose.Entities.LoginUser;

public sealed class LoginUserResponse
    : ApiResponse
{
    public bool IsAuthenticated { get; set; }
    public string? JwtToken { get; set; }
}