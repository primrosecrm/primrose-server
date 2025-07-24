namespace Primrose.API.Validators;

public enum ApiErrorCode : short
{
    InvalidEmailFormat,
    InvalidPasswordFormat,
    InvalidNameFormat,
    UserWithEmailAlreadyExists,
    UserFromEmailDoesNotExist,
    UserForbidden,

    // represents a generic error for when a database item does not exist
    DataNotFound,

    EnumParseFailed,
    UnexpectedException,
}