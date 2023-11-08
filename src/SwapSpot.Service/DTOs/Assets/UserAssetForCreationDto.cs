using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.Service.DTOs.Assets;

public class UserAssetForCreationDto
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsExchanged { get; set; }
}
