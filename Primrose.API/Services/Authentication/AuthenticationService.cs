using Primrose.API.Entities;
using Primrose.API.Entities.Login;
using Primrose.API.Entities.RegisterUser;
using Primrose.API.Repositories;
using Primrose.API.Services.Authentication.Hashing;
using Primrose.API.Services.Authentication.Password;
using Primrose.API.Validators;

namespace Primrose.API.Services.Authentication;

public class AuthenticationService(IUserRepository userRepository, IHashService hashService, IPasswordService passwordService)
    : IAuthenticationService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashService _hashService = hashService;
    private readonly IPasswordService _passwordService = passwordService;

    public async Task<LoginUserResponse> LoginUser(LoginUserRequest request)
    {
        var response = new LoginUserResponse();

        // seems fine to put this in here
        // note that this may cause problems if we change the password policy
        var isValidPassword = _passwordService.CheckPassword(request.Password);
        if (!isValidPassword)
        {
            response.IsAuthenticated = false;
            return response;
        }

        var user = await _userRepository.GetUser(request.Email);
        if (user is null)
        {
            return response.Err(ApiErrorCode.UserFromEmailDoesNotExist);
        }

        var isAuthenticated = _hashService.VerifyHash(request.Password, user.PasswordHash);

        if (isAuthenticated)
        {
            user.LastLogin = DateTime.Now;
            await _userRepository.UpdateUser(user);
        }

        response.IsAuthenticated = isAuthenticated;
        return response;
    }

    public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request)
    {
        var response = new RegisterUserResponse();

        var isValidPassword = _passwordService.CheckPassword(request.Password);
        if (!isValidPassword)
        { 
            return response.Err(ApiErrorCode.InvalidPasswordFormat);
        }

        var passwordHash = _hashService.HashString(request.Password);

        var existingUser = await _userRepository.GetUser(request.Email);
        if (existingUser != null)
        {
            return response.Err(ApiErrorCode.UserWithEmailAlreadyExists);
        }

        var isCreated = await _userRepository.CreateUser(request.Email, request.Name, passwordHash);

        response.CreatedSuccessfully = isCreated;
        return response;
    }
}