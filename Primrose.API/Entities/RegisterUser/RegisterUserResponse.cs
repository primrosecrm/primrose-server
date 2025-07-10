namespace Primrose.API.Entities.RegisterUser;

public sealed class RegisterUserResponse
    : ApiResponse
{
    public bool CreatedSuccessfully { get; set; } = false;
}