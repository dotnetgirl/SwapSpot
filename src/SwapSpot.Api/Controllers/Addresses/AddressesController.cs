using Microsoft.AspNetCore.Mvc;
using SwapSpot.Api.Controllers.Commons;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Addresses;
using SwapSpot.Service.DTOs.Users;
using SwapSpot.Service.Interfaces.Addresses;
using SwapSpot.Service.Interfaces.Users;

namespace SwapSpot.Api.Controllers.Addresses;

public class AddressesController : BaseController
{
    private readonly IAddressService _addressService;
    public AddressesController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _addressService.GetAllAsync(@params)
        });

    [HttpGet("{user-id}/{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "user-id")]long userId, [FromRoute(Name = "id")] long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _addressService.GetByIdAsync(userId, id)
        });

    [HttpPost("{user-id}")]
    public async Task<ActionResult<AddressForResultDto>> PostAsync([FromRoute(Name = "user-id")] long userId, AddressForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _addressService.AddAsync(userId, dto)
        });

    [HttpPut("{user-id}/{id}")]
    public async Task<ActionResult<AddressForResultDto>> PutAsync([FromRoute(Name = "user-id")] long userId, long id, AddressForUpdateDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _addressService.UpdateByIdAsync(userId, id, dto)
        });

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await _addressService.DeleteByIdAsync(id)
        });
}
