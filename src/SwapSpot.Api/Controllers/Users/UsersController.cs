using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwapSpot.Api.Controllers.Commons;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Users;
using SwapSpot.Service.Interfaces.Users;

namespace SwapSpot.Api.Controllers.Users;

public class UsersController : BaseController
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin, SuperAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.RetrieveAllAsync(@params)
        });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.RetrieveByIdAsync(id)
        });

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostAsync(UserForCreationDto dto)
        => Ok(new 
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.AddAsync(dto)
        });

    [Authorize(Roles = "Admin, SuperAdmin, User")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(long id, UserForUpdateDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.ModifyAsync(id, dto)
        });

    [Authorize(Roles = "Admin, SuperAdmin, User")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.RemoveAsync(id)
        });

    [Authorize(Roles = "User")]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePasswordAsync(UserForUpdatePasswordDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.ChangePasswordAsync(dto)
        });

    [Authorize(Roles = "User")]
    [HttpPost("form-file")]
    public async Task<IActionResult> UploadImage(IFormFile formFile)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
        });
}
