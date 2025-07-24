using Primrose.Models;

namespace Primrose.Repositories.Contact;

public interface IContactRepository
{
    public Task<bool> CreateContact(Guid userId, string email, string name);
    public Task<ContactModel?> GetContact(Guid contactId);
    public Task<List<ContactModel>?> GetContacts(Guid userId);
    public Task<bool> UpdateContact(ContactModel contact);
    public Task<bool> DeleteContact(Guid contactId);
}