namespace Primrose.API.Entities;

// This class represents the result of a HTTP API operation.
// Success is set on whether an unexpected error has occurred.
public abstract class ApiResponse
{
    public bool Success { get; set; } = true;
    public string? Error { get; set; } = null;
}