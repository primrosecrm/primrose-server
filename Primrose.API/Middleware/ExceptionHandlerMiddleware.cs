using System.Net;
using System.Text.Json;

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

            var response = new
            {
                success = false,
                message = "An unexpected error occurred.",
                exception = ex.Message,
                innerException = ex.InnerException?.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response, SerializerWriteOptions);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}