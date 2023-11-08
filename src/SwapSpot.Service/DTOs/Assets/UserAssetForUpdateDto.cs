namespace SwapSpot.Service.DTOs.Assets;

public class UserAssetForUpdateDto
{
    public string Name { get; set; }
    public long UserId { get; set; }
    public string Description { get; set; }
    public bool IsExchanged { get; set; }
}