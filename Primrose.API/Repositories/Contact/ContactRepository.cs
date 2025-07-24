using Primrose.Models;
using Supabase;

namespace Primrose.Repositories.Contact;

public class ContactRepository(Client client) : IContactRepository
{
    private readonly Client _client = client;

    public async Task<bool> CreateContact(Guid userId, string email, string name)
    {
        var contact = new ContactModel()
        {
            UserId = userId,
            Email = email,
            Name = name
        };

        var result = await _client.From<ContactModel>()
            .Insert(contact);

        return result.Model != null;
    }

    public async Task<ContactModel?> GetContact(Guid contactId)
    {
        var result = await _client.From<ContactModel>()
            .Where(x => x.ContactId == contactId)
            .Get();

        return result.Model;
    }

    public async Task<List<ContactModel>?> GetContacts(Guid userId)
    {
        var result = await _client.From<ContactModel>()
            .Where(x => x.UserId == userId)
            .Get();

        return result.Models;
    }

    public async Task<bool> UpdateContact(ContactModel contact)
    {
        var result = await _client.From<ContactModel>()
            .Update(contact);

        return result.Model != null;
    }

    public async Task<bool> DeleteContact(Guid contactId)
    {
        if (await GetContact(contactId) is null) return false;

        await _client.From<ContactModel>()
            .Where(x => x.ContactId == contactId)
            .Delete();

        return true;
    }
}
