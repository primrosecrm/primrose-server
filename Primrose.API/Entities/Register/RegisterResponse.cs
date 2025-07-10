namespace Primrose.API.Entities.Register;

public sealed class RegisterResponse
    : ApiResponse
{
    public bool CreatedSuccessfully { get; set; } = false;
}