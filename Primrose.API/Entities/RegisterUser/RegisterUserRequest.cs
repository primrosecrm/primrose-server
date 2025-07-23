namespace Primrose.Entities.RegisterUser;

public sealed class RegisterUserRequest
    : ApiRequest
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
}