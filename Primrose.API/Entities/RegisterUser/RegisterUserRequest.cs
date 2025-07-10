namespace Primrose.API.Entities.RegisterUser;

public sealed class RegisterUserRequest
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
}