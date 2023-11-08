using SwapSpot.Service.DTOs.Messages;

namespace SwapSpot.Service.Interfaces.Users;

public interface IMessageService
{
    Task SendMessageAsync(MessageForCreationDto dto);
}
