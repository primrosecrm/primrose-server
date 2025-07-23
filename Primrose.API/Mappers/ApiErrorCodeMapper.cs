using Primrose.API.Validators;

namespace Primrose.Mappers;

public static class ApiErrorCodeMapper
{
    public static string Map(ApiErrorCode code)
    {
        var message = code switch
        {
            ApiErrorCode.InvalidEmailFormat => "The email provided is malformed.",
            ApiErrorCode.InvalidPasswordFormat => "The password provided does not meet recommended security standards.",
            ApiErrorCode.UnexpectedException => "An unexpected error has occurred on the server.",
            ApiErrorCode.UserWithEmailAlreadyExists => "A user with this email is already registered.",
            ApiErrorCode.UserFromEmailDoesNotExist => "This email address is not currently registered.",
            ApiErrorCode.InvalidNameFormat => "A user name must be at least three characters.",
            ApiErrorCode.EnumParseFailed => "Failed to parse error code as enum. This is likely an issue with one of the validators for this request.",
            ApiErrorCode.UserForbidden => "The user attempted to modify another users record. Basically, the email in the JWT did not match the user being altered.",
            _ => "Error code not handled in message mapper. Notify developer."
        };

        return message;
    }
}