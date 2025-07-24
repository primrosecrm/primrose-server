using Primrose.Models;

namespace Primrose.Entities.CreateContact;

public sealed class UpdateContactRequest
    : ApiRequest
{
    public required ContactModel Model { get; set; }
}