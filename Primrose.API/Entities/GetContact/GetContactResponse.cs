using Primrose.Models;

namespace Primrose.Entities.GetContact;

public sealed class GetContactResponse
    : ApiResponse
{
    public ContactModel? Model { get; set; }
}