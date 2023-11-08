using SwapSpot.DAL.DbContexts;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.Repositories.Commons;
using SwapSpot.Domain.Entities.Assets;

namespace SwapSpot.DAL.Repositories;

public class UserAssetPhotoRepository : Repository<UserAssetPhoto>, IUserAssetPhotoRepository
{
    public UserAssetPhotoRepository(SwapSpotDbContext dbContext) : base(dbContext)
    {
    }
}
