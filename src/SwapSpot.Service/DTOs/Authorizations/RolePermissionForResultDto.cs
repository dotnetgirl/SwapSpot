using SwapSpot.Service.DTOs.Authorizations.Permissions;
using SwapSpot.Service.DTOs.Authorizations.Roles;

namespace SwapSpot.Service.DTOs.Authorizations;

public class RolePermissionForResultDto
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public RoleForResultDto Role { get; set; }

    public long PermissonId { get; set; }
    public PermissionForResultDto Permisson { get; set; }
}
