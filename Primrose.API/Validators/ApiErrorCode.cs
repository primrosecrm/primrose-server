namespace Primrose.API.Validators;

public enum ApiErrorCode : short
{
    InvalidEmailFormat,
    InvalidPasswordFormat,
    InvalidNameFormat,
    UserWithEmailAlreadyExists,
    UserFromEmailDoesNotExist,
    UserForbidden,

    EnumParseFailed,
    UnexpectedException,
}