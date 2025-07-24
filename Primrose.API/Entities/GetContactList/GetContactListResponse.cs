using Primrose.Models;

namespace Primrose.Entities.GetContactList;

public sealed class GetContactListResponse
    : ApiResponse
{
    public List<ContactModel>? Models { get; set; }
}