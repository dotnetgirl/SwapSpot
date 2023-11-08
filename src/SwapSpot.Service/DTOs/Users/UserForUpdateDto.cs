using Microsoft.AspNetCore.Http;
using SwapSpot.Domain.Authorizations;
using SwapSpot.Domain.Entities.Addresses;
using System.ComponentModel.DataAnnotations;

namespace SwapSpot.Service.DTOs.Users;

public class UserForUpdateDto
{
    [MinLength(3), MaxLength(50)]
    public string FirstName { get; set; }
    [MinLength(3), MaxLength(50)]
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}