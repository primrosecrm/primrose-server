namespace Primrose.API.Services.Authentication.Pasword;

public interface IPasswordPolicy
{
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
}