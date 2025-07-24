using System.Security.Claims;
using Primrose.API.Validators;
using Primrose.Entities;
using Primrose.Entities.DeactivateUser;
using Primrose.Repositories.User;

namespace Primrose.Services.User;

public class UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string? GetEmailFromJwt()
    {
        var jwtTokenEmail = _httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type is ClaimTypes.Email)?.Value;

        return jwtTokenEmail;
    }

    public async Task<DeactivateUserResponse> DeactivateUser(DeactivateUserRequest request)
    {
        var response = new DeactivateUserResponse();

        var user = await _userRepository.GetUser(request.Email);
        if (user is null)
        {
            return response.Err(ApiErrorCode.UserFromEmailDoesNotExist);
        }

        var jwtTokenEmail = GetEmailFromJwt();
        if (jwtTokenEmail != user.Email)
        {
            return response.Err(ApiErrorCode.UserForbidden);
        }

        user.IsActive = false;

        var isUpdated = await _userRepository.UpdateUser(user);

        response.IsDeactivated = isUpdated;
        return response;
    }

    public async Task<ActivateUserResponse> ActivateUser(ActivateUserRequest request)
    {
        var response = new ActivateUserResponse();

        var user = await _userRepository.GetUser(request.Email);
        if (user is null)
        {
            return response.Err(ApiErrorCode.UserFromEmailDoesNotExist);
        }

        var jwtTokenEmail = GetEmailFromJwt();
        if (jwtTokenEmail != user.Email)
        {
            return response.Err(ApiErrorCode.UserForbidden);
        }

        user.IsActive = true;

        var isUpdated = await _userRepository.UpdateUser(user);

        response.IsActivated = isUpdated;
        return response;
    }
}