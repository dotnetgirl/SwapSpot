using SwapSpot.DAL.DbContexts;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.Repositories.Commons;
using SwapSpot.Domain.Entities.Messages;

namespace SwapSpot.DAL.Repositories;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(SwapSpotDbContext dbContext) : base(dbContext)
    {
    }
}