using SwapSpot.Domain.Commons;
using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.Domain.Entities.Messages;

public class Message : Auditable
{
    public long From { get; set; }
    public User FromUser { get; set; }

    public long To { get; set; }
    public User ToUser { get; set; }

    public string Subject { get; set; }
    public string Body { get; set; }
}