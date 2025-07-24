namespace Primrose.Entities.GetContact;

public sealed class GetContactListRequest
    : ApiRequest
{
    public required Guid UserId { get; set; }
}