using SwapSpot.DAL.DbContexts;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.Repositories.Commons;
using SwapSpot.Domain.Entities.Assets;

namespace SwapSpot.DAL.Repositories;

public class UserAssetRepository : Repository<UserAsset>, IUserAssetRepository
{
    public UserAssetRepository(SwapSpotDbContext dbContext) : base(dbContext)
    {
    }
}