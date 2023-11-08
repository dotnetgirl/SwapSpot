using SwapSpot.DAL.DbContexts;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.Repositories.Commons;
using SwapSpot.Domain.Entities.Photos;

namespace SwapSpot.DAL.Repositories;

public class UserPhotoRepository : Repository<Photo>, IUserPhotoRepository
{
    public UserPhotoRepository(SwapSpotDbContext dbContext) : base(dbContext)
    {
    }
}
