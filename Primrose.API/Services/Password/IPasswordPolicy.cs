namespace Primrose.API.Services.Authentication.Password;

public interface IPasswordPolicy
{
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
}