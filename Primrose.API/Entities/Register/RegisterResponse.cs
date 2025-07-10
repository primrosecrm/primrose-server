namespace Primrose.API.Entities.Login;

public sealed class RegisterResponse
    : ApiResponse
{
    public bool CreatedSuccessfully { get; set; } = false;
}