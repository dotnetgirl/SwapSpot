using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.Service.DTOs.Assets;

public class UserAssetForUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsExchanged { get; set; }
}