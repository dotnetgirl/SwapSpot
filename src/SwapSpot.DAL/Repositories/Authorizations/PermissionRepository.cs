using SwapSpot.DAL.DbContexts;
using SwapSpot.DAL.IRepositories.Authorizations;
using SwapSpot.DAL.Repositories.Commons;
using SwapSpot.Domain.Authorizations;

namespace SwapSpot.DAL.Repositories.Authorizations;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(SwapSpotDbContext dbContext) : base(dbContext)
    {
    }
}
