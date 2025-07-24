using System.Formats.Asn1;
using Primrose.API.Validators;
using Primrose.Entities;
using Primrose.Entities.CreateContact;
using Primrose.Entities.DeleteContact;
using Primrose.Entities.GetContact;
using Primrose.Entities.GetContactList;
using Primrose.Repositories.Contact;

namespace Primrose.Services.Contact;

public class ContactService(IContactRepository contactRepository, IHttpContextAccessor httpContextAccessor)
    : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid? GetUserIdFromJwt()
    {
        // ugly as hell and probably a bad way to do it
        var jwtTokenEmail = _httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type is "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        var isValid = Guid.TryParse(jwtTokenEmail, out Guid id);
        return isValid ? id : null;
    }

    public async Task<CreateContactResponse> CreateContact(CreateContactRequest request)
    {
        var response = new CreateContactResponse();

        var userId = GetUserIdFromJwt();
        if (userId is null)
        {
            return response.Err(ApiErrorCode.UserForbidden);
        }

        var result = await _contactRepository.CreateContact(userId.Value, request.Email, request.Name);

        response.IsCreated = result;
        return response;
    }

    public async Task<GetContactResponse> GetContact(GetContactRequest request)
    {
        var response = new GetContactResponse();

        var contact = await _contactRepository.GetContact(request.ContactId);
        if (contact is null)
        {
            return response.Err(ApiErrorCode.DataNotFound);
        }

        response.Model = contact;

        return response;
    }

    public async Task<GetContactListResponse> GetContactList(GetContactListRequest request)
    {
        var response = new GetContactListResponse();

        var contact = await _contactRepository.GetContacts(request.UserId);
        if (contact is null)
        {
            return response.Err(ApiErrorCode.DataNotFound);
        }

        response.Models = contact;

        return response;
    }

    public async Task<DeleteContactResponse> DeleteContact(DeleteContactRequest request)
    {
        var response = new DeleteContactResponse();

        var isDeleted = await _contactRepository.DeleteContact(request.ContactId);
        if (!isDeleted)
        {
            return response.Err(ApiErrorCode.DataNotFound);
        }

        response.IsDeleted = isDeleted;

        return response;
    }
    
        public async Task<UpdateContactResponse> UpdateContact(UpdateContactRequest request)
    {
        var response = new UpdateContactResponse();

        var isUpdated = await _contactRepository.UpdateContact(request.Model);
        if (!isUpdated)
        { 
            return response.Err(ApiErrorCode.DataNotFound);
        }

        response.IsUpdated = isUpdated;

        return response;
    }
}
