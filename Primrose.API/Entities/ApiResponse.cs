using Primrose.API.Mappers;
using Primrose.API.Validators;
using Primrose.API.Validators.Services;

namespace Primrose.API.Entities;

// This class represents the result of a HTTP API operation.
// Success is set on whether an unexpected error has occurred.
public class ApiResponse
{
    public bool Success { get; set; } = true;
    public ApiResult ErrorResult { get; set; } = new();

    public T Err<T>(ApiErrorCode code)
        where T : ApiResponse
    {
        var message = ApiErrorCodeMapper.Map(code);

        ErrorResult.Errors.Add(new ApiError(message, code.ToString()));
        return (T)this;
    }
}