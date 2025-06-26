namespace Primrose.API.Services.Authentication;

public interface IHashService
{
    public string HashString(string str);
    public bool VerifyHash(string str, string hashedStr);
}