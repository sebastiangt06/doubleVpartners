using System.Security.Claims;

namespace DoubleV.Application.Common.Interfaces
{
    public interface IJwtTokenService
    {
        string CreateToken(IEnumerable<Claim> claims);
    }
}
