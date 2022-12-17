using Utilities.DTOs;

namespace Utilities.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResult> GetLoginResultAsync(string userId, string name, string email);
    }
}
