using Microsoft.AspNetCore.Mvc;
using SwapSpot.Api.Controllers.Commons;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Authorizations.Permissions;
using SwapSpot.Service.Interfaces.Authorizations;

namespace SwapSpot.Api.Controllers.Authorizations;

public class PermissionsController : BaseController
{
    private readonly IPermissionService permissionService;

    public PermissionsController(IPermissionService permissionService)
    {
        this.permissionService = permissionService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(PermissionForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.permissionService.CreateAsync(dto)
        });

    [HttpPut]
    public async Task<IActionResult> PutAsync(PermissionForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.permissionService.ModifyAsync(dto)
        });


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.permissionService.RemoveAsync(id)
        });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(long id)
      => Ok(new
      {
          Code = 200,
          Message = "OK",
          Data = await this.permissionService.RetrieveByIdAsync(id)
      });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
      => Ok(new
      {
          Code = 200,
          Message = "OK",
          Data = await this.permissionService.RetrieveAllAsync(@params)
      });
}
