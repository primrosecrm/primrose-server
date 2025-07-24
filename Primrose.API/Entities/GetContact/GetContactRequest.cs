namespace Primrose.Entities.GetContact;

public sealed class GetContactRequest
    : ApiRequest
{
    public required Guid ContactId { get; set; }
}