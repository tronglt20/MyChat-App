using Domain.Entities;
using Domain.Interfaces.Base;

namespace Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetAsync(string email);
        Task<bool> CheckPasswordAsync(int userId, string password);
    }
}
