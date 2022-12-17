using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> CheckPasswordAsync(int userId, string password)
        {
            return AnyAsync(_ => _.Id == userId && _.Password == password);
        }

        public async Task<User> GetAsync(string email)
        {
            return await GetAsync(_ => _.Email == email);
        }
    }
}
