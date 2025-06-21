namespace Primrose.API.Services.Authentication;


public interface IAuthenticationService<TCredentials, TResult>
    where TCredentials : class
    where TResult : class
{
    public TResult Authenticate(TCredentials credentials);
}