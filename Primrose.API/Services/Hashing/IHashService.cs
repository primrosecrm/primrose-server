namespace Primrose.Services.Hashing;

public interface IHashService
{
    public string HashString(string str);
    public bool VerifyHash(string str, string hashedStr);
}