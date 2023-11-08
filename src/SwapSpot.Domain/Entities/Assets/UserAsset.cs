using Microsoft.AspNetCore.Http;
using SwapSpot.Domain.Commons;
using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.Domain.Entities.Assets;

public class UserAsset : Auditable
{
    public long? UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsExchanged { get; set; }
}