using SwapSpot.Domain.Commons;

namespace SwapSpot.Domain.Authorizations;

public class Permission : Auditable
{
    public string Name { get; set; }
}
