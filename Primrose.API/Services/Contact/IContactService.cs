using Primrose.Entities.CreateContact;
using Primrose.Entities.DeleteContact;
using Primrose.Entities.GetContact;
using Primrose.Entities.GetContactList;

namespace Primrose.Services.Contact;

public interface IContactService
{
    public Task<CreateContactResponse> CreateContact(CreateContactRequest request);
    public Task<GetContactResponse> GetContact(GetContactRequest request);
    public Task<GetContactListResponse> GetContactList(GetContactListRequest request);
    public Task<DeleteContactResponse> DeleteContact(DeleteContactRequest request);
    public Task<UpdateContactResponse> UpdateContact(UpdateContactRequest request);
}