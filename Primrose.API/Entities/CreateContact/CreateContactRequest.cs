namespace Primrose.Entities.CreateContact;

public sealed class CreateContactRequest
    : ApiRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}