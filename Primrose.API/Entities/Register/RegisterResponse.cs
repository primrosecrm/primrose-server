namespace Primrose.API.Entities.Login;

public sealed class RegisterResponse(bool createdSuccessfully) 
    : ApiResponse
{
    public bool CreatedSuccessfully { get; set; } = createdSuccessfully;
}