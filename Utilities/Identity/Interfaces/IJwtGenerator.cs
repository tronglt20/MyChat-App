using System.Security.Claims;

namespace Utilities.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateAccessToken(params Claim[] claims);
    }
}
