using Microsoft.AspNetCore.Http;
using SwapSpot.Domain.Authorizations;
using SwapSpot.Domain.Commons;
using SwapSpot.Domain.Entities.Addresses;
using SwapSpot.Domain.Entities.Assets;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SwapSpot.Domain.Entities.Users;

public class User : Auditable
{
    [MinLength(3), MaxLength(50)]
    public string FirstName { get; set; }
    [MinLength(3), MaxLength(50)]
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }

    [JsonIgnore]
    public ICollection<UserAsset> Assets { get; set; }
}
