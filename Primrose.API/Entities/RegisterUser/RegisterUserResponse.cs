namespace Primrose.Entities.RegisterUser;

public sealed class RegisterUserResponse
    : ApiResponse
{
    public bool CreatedSuccessfully { get; set; } = false;
}