using SwapSpot.Domain.Entities.Photos;

namespace SwapSpot.Domain.Entities.Assets;

public class UserAssetPhoto : Photo
{
    public long? UserAssetId { get; set; }
    public UserAsset UserAsset { get; set; }
}