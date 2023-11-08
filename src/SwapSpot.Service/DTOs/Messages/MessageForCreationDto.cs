using SwapSpot.Domain.Entities.Users;

namespace SwapSpot.Service.DTOs.Messages;

public class MessageForCreationDto
{
    public long To { get; set; }
    public User User { get; set; }

    public string Subject { get; set; }
    public string Body { get; set; }
}
