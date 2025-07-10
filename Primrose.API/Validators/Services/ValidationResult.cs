namespace Primrose.API.Validators.Services;

// library‑agnostic validation result
public sealed record ApiError(string Message, string Code);

public sealed class ApiResult
{
    public List<ApiError> Errors { get; set; } = [];
}