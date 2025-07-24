using Primrose.Entities.DeactivateUser;

namespace Primrose.Services.User;

public interface IUserService
{
    Task<DeactivateUserResponse> DeactivateUser(DeactivateUserRequest request);
    Task<ActivateUserResponse> ActivateUser(ActivateUserRequest request);
}