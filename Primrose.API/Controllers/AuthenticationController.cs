using Microsoft.AspNetCore.Mvc;
using Primrose.API.Models.Authentication;
using Primrose.API.Models.Login;
using Primrose.API.Services.Authentication;

namespace Primrose.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService<UserCredential, UserAuthenticationResult> _authenticationService;

    public AuthenticationController(IAuthenticationService<UserCredential, UserAuthenticationResult> authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Login")]
    public ActionResult Login(LoginRequest request)
    {
        var userCredentials = new UserCredential(request.Username, request.Password);
        var result = _authenticationService.Authenticate(userCredentials);

        var response = new LoginResponse(result);

        return Ok(response);
    }
}