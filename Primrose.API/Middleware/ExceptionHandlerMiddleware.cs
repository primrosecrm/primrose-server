using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Primrose.API.Validators;
using Primrose.Entities;
using Primrose.Validators.Services;

namespace Primrose.Middleware;

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

            var apiResponse = new BadResponse
            {
                Success = false,
                ErrorResult = new()
                { 
                    Errors = [
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

            var jsonString = System.Text.Json.JsonSerializer.Serialize(apiResponse, SerializerWriteOptions);

            var parsedJson = JToken.Parse(jsonString);
            string prettyJson = parsedJson.ToString(Formatting.Indented);

            await context.Response.WriteAsync(prettyJson);
        }
    }
}