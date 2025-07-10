namespace Primrose.API.Services.Authentication.Hashing;

public class BCryptHashService : IHashService
{
    public string HashString(string str)
    {
        return BCrypt.Net.BCrypt.HashPassword(str);
    }

    public bool VerifyHash(string str, string hashedStr)
    {
        return BCrypt.Net.BCrypt.Verify(str, hashedStr);
    }
}