using SwapSpot.DAL.DbContexts;
using SwapSpot.DAL.IRepositories.Authorizations;
using SwapSpot.DAL.Repositories.Commons;
using SwapSpot.Domain.Authorizations;

namespace SwapSpot.DAL.Repositories.Authorizations;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(SwapSpotDbContext dbContext) : base(dbContext)
    {
    }
}