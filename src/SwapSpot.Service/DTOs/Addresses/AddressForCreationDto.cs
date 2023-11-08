using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.Service.DTOs.Addresses;

public class AddressForCreationDto
{
    public string City { get; set; }
    public string Street { get; set; }
    public string Floor { get; set; }
    public string Home { get; set; }
}