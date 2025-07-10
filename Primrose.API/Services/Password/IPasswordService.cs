namespace Primrose.API.Services.Authentication.Password;

public interface IPasswordService
{
    bool CheckPassword(string password);
}
