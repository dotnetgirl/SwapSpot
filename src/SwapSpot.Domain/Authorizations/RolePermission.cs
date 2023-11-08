using SwapSpot.Domain.Commons;

namespace SwapSpot.Domain.Authorizations;

public class RolePermission : Auditable
{
    public long RoleId { get; set; }
    public Role Role { get; set; }

    public long PermissonId { get; set; }
    public Permission Permisson { get; set; }
}