using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Primrose.Entities.CreateContact;
using Primrose.Services.Contact;
using Primrose.Entities.GetContact;
using Primrose.Entities.DeleteContact;

namespace Primrose.Controllers;

[ApiController]
[ApiVersion("1.0")]
[RequireHttps]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public class ContactController(IContactService contactService)
    : PrimroseApiController
{
    private readonly IContactService _contactService = contactService;

    [HttpPost("Create")]
    public async Task<ActionResult<CreateContactResponse>> CreateContact(CreateContactRequest request)
    {
        return ApiResult(await _contactService.CreateContact(request));
    }

    [HttpPost("Get")]
    public async Task<ActionResult<CreateContactResponse>> GetContact(GetContactRequest request)
    {
        return ApiResult(await _contactService.GetContact(request));
    }

    [HttpPost("GetList")]
    public async Task<ActionResult<CreateContactResponse>> GetContactList(GetContactListRequest request)
    {
        return ApiResult(await _contactService.GetContactList(request));
    }

    [HttpPost("Delete")]
    public async Task<ActionResult<DeleteContactResponse>> DeleteContact(DeleteContactRequest request)
    {
        return ApiResult(await _contactService.DeleteContact(request));
    }
    
    [HttpPost("Update")]
    public async Task<ActionResult<UpdateContactResponse>> UpdateContact(UpdateContactRequest request)
    {
        return ApiResult(await _contactService.UpdateContact(request));
    }
}