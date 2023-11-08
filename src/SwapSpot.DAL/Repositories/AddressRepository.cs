using SwapSpot.DAL.DbContexts;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.Repositories.Commons;
using SwapSpot.Domain.Entities.Addresses;

namespace SwapSpot.DAL.Repositories;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(SwapSpotDbContext dbContext) : base(dbContext)
    {
    }
}
