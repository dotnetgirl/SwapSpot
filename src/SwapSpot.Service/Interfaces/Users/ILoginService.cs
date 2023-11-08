using SwapSpot.Service.DTOs.Users.Logins;

namespace SwapSpot.Service.Interfaces.Users;

public interface ILoginService
{
    Task<UserLoginResultDto> LoginAsync(UserForLoginDto userForLoginDto);
}
