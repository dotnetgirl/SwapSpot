using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.Service.DTOs.Assets;

public class UserAssetForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsExchanged { get; set; }
}