
using FluentValidation.Results;
using Primrose.API.Validators.Services;

namespace Primrose.API.Entities.Login;

public sealed class LoginResponse
    : ApiResponse
{
    public bool IsAuthenticated { get; set; }

    public LoginResponse(bool isAuthenticated, ApiValidationResult? result = null)
    {
        IsAuthenticated = isAuthenticated;
        Result = result;
    }
}