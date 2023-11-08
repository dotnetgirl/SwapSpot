using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.Service.DTOs.Addresses;

public class AddressForCreationDto
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Floor { get; set; }
    public string Home { get; set; }
}