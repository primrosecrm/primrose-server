using Primrose.API.Repositories;

namespace Primrose.API.Services.Authentication;

public class AuthenticationService
{
    private readonly UserRepository _userRepository;
    private readonly IHashService _hashService;

    public AuthenticationService(UserRepository userRepository, IHashService hashService)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public async Task<bool> LoginUser(string email, string password)
    {
        var user = await _userRepository.GetUser(email);
        if (user is null) return false;

        var isVerified = _hashService.VerifyHash(password, user.PasswordHash);
        return isVerified;
    }

    public async Task<bool> RegisterUser(string email, string name, string password)
    {
        var passwordHash = _hashService.HashString(password);

        var isCreated = await _userRepository.CreateUser(email, name, passwordHash);
        return isCreated;
    }
}