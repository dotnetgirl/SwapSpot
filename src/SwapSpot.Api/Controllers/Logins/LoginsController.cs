using Microsoft.AspNetCore.Mvc;
using SwapSpot.Api.Controllers.Commons;
using SwapSpot.Service.DTOs.Users.Logins;
using SwapSpot.Service.Interfaces.Users;

namespace SwapSpot.Api.Controllers.Logins;

public class LoginsController : BaseController
{
    private readonly ILoginService _loginService;

    public LoginsController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync(UserForLoginDto dto)
    {
        return Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await _loginService.LoginAsync(dto)
        });
    }
}
