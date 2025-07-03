namespace Primrose.API.Entities.Login;

public sealed class RegisterRequest
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
}