using Primrose.API.Mappers;
using Primrose.API.Validators;

namespace Primrose.API.Entities;

public static class ApiResponseExtensions
{
    public static T Err<T>(this T response, ApiErrorCode code)
        where T : ApiResponse
    {
        var message = ApiErrorCodeMapper.Map(code);
        response.ErrorResult.Errors.Add(new(message, code.ToString()));

        return response;
    }
}