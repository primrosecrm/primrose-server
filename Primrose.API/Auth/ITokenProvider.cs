using Primrose.Models.Authentication;

namespace Primrose.Auth;

public interface ITokenProvider
{
    public string Create(PrimroseUser user);
}