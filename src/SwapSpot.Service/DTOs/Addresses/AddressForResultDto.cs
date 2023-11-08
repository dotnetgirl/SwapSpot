using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.Service.DTOs.Addresses;

public class AddressForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Floor { get; set; }
    public string Home { get; set; }
}
