using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRegistrationReposotory : BaseRepository<UserRegistration>, IUserRegistrationRepository
    {
        public UserRegistrationReposotory(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
