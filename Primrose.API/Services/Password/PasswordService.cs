namespace Primrose.Services.Password;

public class PasswordService(IPasswordPolicy policy)
    : IPasswordService
{
    private readonly IPasswordPolicy _policy = policy;

    public bool CheckPassword(string password)
    {
        if (password.Length < _policy.MinLength) return false;
        if (password.Length > _policy.MaxLength) return false;

        var hasUpper = password.Any(char.IsUpper);
        var hasLower = password.Any(char.IsLower);
        var hasDigit = password.Any(char.IsDigit);
        var hasSymbol = password.Any((x) => !char.IsLetterOrDigit(x));

        var isValid = hasUpper && hasLower && hasDigit && hasSymbol;
        return isValid;
    }
}