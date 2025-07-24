namespace Primrose.Entities.DeleteContact;

public sealed class DeleteContactRequest
    : ApiRequest
{
    public required Guid ContactId { get; set; }
}