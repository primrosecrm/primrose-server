namespace Primrose.Entities.DeactivateUser;

public sealed class DeactivateUserRequest
    : ApiRequest
{
    public required string Email { get; set; }
}