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

    [HttpPost]
    public async Task<IActionResult> PostAsync(UserForCreationDto dto)
        => Ok(new 
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.AddAsync(dto)
        });

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(long id, UserForUpdateDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.ModifyAsync(id, dto)
        });

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.RemoveAsync(id)
        });

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePasswordAsync(UserForUpdatePasswordDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.ChangePasswordAsync(dto)
        });
    [HttpPost("form-file")]
    public async Task<IActionResult> UploadImage(IFormFile formFile)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userService.UploadImage(formFile)
        });
}
