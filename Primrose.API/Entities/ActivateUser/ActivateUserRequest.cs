namespace Primrose.Entities.DeactivateUser;

public sealed class ActivateUserRequest
    : ApiRequest
{
    public required string Email { get; set; }
}