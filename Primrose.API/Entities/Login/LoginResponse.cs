
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Primrose.API.Validators.Services;

namespace Primrose.API.Entities.Login;


public sealed class LoginResponse
    : ApiResponse
{
    private LoginResponse() { }

    public required bool IsAuthenticated { get; set; }

    public static IActionResult Ok(bool isAuthenticated)
    {
        var response = new LoginResponse
        {
            IsAuthenticated = isAuthenticated
        };

        return new OkObjectResult(response);
    }

    public static IActionResult Bad(ApiValidationResult? result = null)
    {
        var response = new LoginResponse
        {
            IsAuthenticated = false,
            Result = result
        };

        return new BadRequestObjectResult(response);
    }
}