namespace Primrose.API.Models.Authentication;

public sealed class UserCredential(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}