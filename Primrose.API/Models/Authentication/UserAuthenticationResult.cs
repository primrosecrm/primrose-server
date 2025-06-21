namespace Primrose.API.Models.Authentication;

public sealed class UserAuthenticationResult
{
    public bool IsAuthenticated { get; set; }

    private UserAuthenticationResult(bool isAuthenticated)
    {
        IsAuthenticated = isAuthenticated;
    }

    public static UserAuthenticationResult Ok()
    {
        return new(true);
    }

    public static UserAuthenticationResult Err()
    {
        return new(false);
    }
}