using SwapSpot.DAL.DbContexts;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.Repositories.Commons;
using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.DAL.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(SwapSpotDbContext dbContext) : base(dbContext)
    {
    }
}
