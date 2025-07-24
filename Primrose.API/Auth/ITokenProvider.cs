using Primrose.Models;

namespace Primrose.Auth;

public interface ITokenProvider
{
    public string Create(PrimroseUser user);
}