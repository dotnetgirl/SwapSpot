using SwapSpot.DAL.DbContexts;
using SwapSpot.DAL.IRepositories.Authorizations;
using SwapSpot.DAL.Repositories.Commons;
using SwapSpot.Domain.Authorizations;

namespace SwapSpot.DAL.Repositories.Authorizations;

public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
{
    public RolePermissionRepository(SwapSpotDbContext dbContext) : base(dbContext)
    {
    }
}
