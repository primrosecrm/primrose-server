namespace Primrose.Services.Password;

public class OwaspPasswordPolicy : IPasswordPolicy
{
    public int MinLength { get; set; } = 8;
    public int MaxLength { get; set; } = 64;
}