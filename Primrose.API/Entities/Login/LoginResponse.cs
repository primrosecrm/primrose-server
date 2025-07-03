namespace Primrose.API.Entities.Login;

public sealed class LoginResponse(bool result) 
    : ApiResponse
{
    public bool IsAuthenticated { get; set; } = result;
}