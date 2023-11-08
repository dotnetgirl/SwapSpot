using Microsoft.AspNetCore.Mvc;
using SwapSpot.Api.Controllers.Commons;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Assets;
using SwapSpot.Service.Interfaces.Assets;

namespace SwapSpot.Api.Controllers.Assets;

public class UserAssetsController : BaseController
{
    private readonly IUserAssetService _userAssetService;
    public UserAssetsController(IUserAssetService userAssetService)
    {
        _userAssetService = userAssetService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userAssetService.RetrieveAllAsync(@params)
        });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userAssetService.RetrieveByIdAsync(id)
        });

    [HttpPost]
    public async Task<IActionResult> PostAsync(UserAssetForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userAssetService.AddAsync(dto)
        });

    [HttpPut("{user-id}/{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "user-id")] long userId, long id, UserAssetForUpdateDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userAssetService.ModifyAsync(userId, id, dto)
        });

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userAssetService.RemoveAsync(id)
        });
    [HttpPost("{id}")]
    public async Task<IActionResult> UploadImage(long id, IFormFile formFile)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _userAssetService.UploadImage(id, formFile)
        });
}
