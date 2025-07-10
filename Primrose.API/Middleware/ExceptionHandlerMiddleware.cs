using System.Net;
using System.Text.Json;
using Primrose.API.Entities;
using Primrose.API.Validators;
using Primrose.API.Validators.Services;

namespace Primrose.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private static readonly JsonSerializerOptions SerializerWriteOptions = new()
    {
        WriteIndented = true
    };

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var apiResponse = new ApiResponse
            {
                Success = false,
                ErrorResult = new()
                { 
                    Errors = [
                        // TODO: remove escape characters from this error message - it looks ugly.
                        new ApiError(
                            $"Exception: {ex.Message}",
                            ApiErrorCode.UnexpectedException.ToString()
                        ),
                        new ApiError(
                            $"Inner Exception: {ex.InnerException?.Message ?? "No inner exception"}",
                            ApiErrorCode.UnexpectedException.ToString()
                        )
                    ]
                }
            };

            var jsonResponse = JsonSerializer.Serialize(apiResponse, SerializerWriteOptions);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}