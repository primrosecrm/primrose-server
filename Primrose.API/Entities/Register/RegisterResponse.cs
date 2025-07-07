using Microsoft.AspNetCore.Mvc;
using Primrose.API.Validators.Services;

namespace Primrose.API.Entities.Login;

public sealed class RegisterResponse
    : ApiResponse
{
    private RegisterResponse() { }

    public required bool CreatedSuccessfully { get; set; } = false;

    public static IActionResult Ok(bool createdSuccessfully)
    {
        var response = new RegisterResponse
        {
            CreatedSuccessfully = createdSuccessfully
        };

        return new OkObjectResult(response);
    }

    public static IActionResult Bad(ApiValidationResult? result = null)
    {
        var response = new RegisterResponse
        {
            CreatedSuccessfully = false,
            Result = result
        };

        return new BadRequestObjectResult(response);
    }
}