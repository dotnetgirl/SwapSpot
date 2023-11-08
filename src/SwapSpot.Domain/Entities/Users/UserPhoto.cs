using SwapSpot.Domain.Entities.Photos;

namespace SwapSpot.Domain.Entities.Users;

public class UserPhoto : Photo
{
    public long? UserId { get; set; }
    public User User { get; set; }
}
