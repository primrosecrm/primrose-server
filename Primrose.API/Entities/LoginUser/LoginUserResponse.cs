namespace Primrose.API.Entities.Login;

public sealed class LoginUserResponse
    : ApiResponse
{
    public bool IsAuthenticated { get; set; }
}