using SwapSpot.Service.DTOs.Authorizations.Permissions;
using SwapSpot.Service.DTOs.Authorizations.Roles;

namespace SwapSpot.Service.DTOs.Authorizations;

public class RolePermissionForCreationDto
{
    public long RoleId { get; set; }
    public long PermissonId { get; set; }
}