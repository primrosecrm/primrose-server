namespace Primrose.API.Models.Login;

public sealed class RegisterResponse(bool createdSuccessfully) 
    : ApiResponse
{
    public bool CreatedSuccessfully { get; set; } = createdSuccessfully;
}