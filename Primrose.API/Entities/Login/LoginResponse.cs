namespace Primrose.API.Entities.Login;

public sealed class LoginResponse
    : ApiResponse
{
    public bool IsAuthenticated { get; set; }
}