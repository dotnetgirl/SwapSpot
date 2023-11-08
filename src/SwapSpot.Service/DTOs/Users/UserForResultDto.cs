using SwapSpot.Domain.Entities.Assets;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SwapSpot.Service.DTOs.Users;

public class UserForResultDto
{
    public long Id { get; set; }

    [DisplayName("FirstName")]
    public string FirstName { get; set; }

    [DisplayName("LastName")]
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    [JsonIgnore]
    public ICollection<UserAsset> Assets { get; set; }
}
