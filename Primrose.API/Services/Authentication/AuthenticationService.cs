using Primrose.API.Entities.Login;
using Primrose.API.Repositories;

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

    public async Task<bool> LoginUser(LoginRequest request)
    {
        var user = await _userRepository.GetUser(request.Email);
        if (user is null) return false;

        var isVerified = _hashService.VerifyHash(request.Password, user.PasswordHash);
        return isVerified;
    }

    public async Task<bool> RegisterUser(RegisterRequest request)
    {
        var passwordHash = _hashService.HashString(request.Password);

        var isCreated = await _userRepository.CreateUser(request.Email, request.Name, passwordHash);
        return isCreated;
    }
}