using Primrose.API.Entities.Login;
using Primrose.API.Entities.Register;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginUser(LoginRequest request);
    Task<RegisterResponse> RegisterUser(RegisterRequest request);
}
