using SwapSpot.Domain.Commons;

namespace SwapSpot.Domain.Authorizations;

public class Role : Auditable
{
    public string Name { get; set; }
}