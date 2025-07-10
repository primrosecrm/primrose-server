using Primrose.API.Entities.Login;
using Primrose.API.Repositories;
using Primrose.API.Validators;

namespace Primrose.API.Services.Authentication;

public class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;

    public AuthenticationService(IUserRepository userRepository, IHashService hashService)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public async Task<LoginResponse> LoginUser(LoginRequest request)
    {
        var response = new LoginResponse();

        var user = await _userRepository.GetUser(request.Email);
        if (user is null)
        {
            return response.Err<LoginResponse>(ApiErrorCode.UserFromEmailDoesNotExist);
        }

        var isAuthenticated = _hashService.VerifyHash(request.Password, user.PasswordHash);

        response.IsAuthenticated = isAuthenticated;
        return response;
    }

    public async Task<RegisterResponse> RegisterUser(RegisterRequest request)
    {
        var response = new RegisterResponse();

        var passwordHash = _hashService.HashString(request.Password);

        var existingUser = await _userRepository.GetUser(request.Email);
        if (existingUser != null)
        {
            return response.Err<RegisterResponse>(ApiErrorCode.UserWithEmailAlreadyExists);
        }

        var isCreated = await _userRepository.CreateUser(request.Email, request.Name, passwordHash);

        response.CreatedSuccessfully = isCreated;
        return response;
    }
}