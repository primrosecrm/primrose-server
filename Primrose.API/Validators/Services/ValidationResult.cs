namespace Primrose.API.Validators.Services;

// library‑agnostic validation result
public sealed record ApiValidationError(string Property, string Message);

public sealed class ApiValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public List<ApiValidationError> Errors { get; } = [];
}
