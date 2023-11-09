using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwapSpot.Api.Controllers.Commons;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Authorizations;
using SwapSpot.Service.DTOs.Authorizations.Roles;
using SwapSpot.Service.Interfaces.Authorizations;

namespace SwapSpot.Api.Controllers.Authorizations;

[Authorize(Roles = "Admin, SuperAdmin")]
public class RolePermissionsController : BaseController
{
    private readonly IRolePermissionService rolePermissionService;

    public RolePermissionsController(IRolePermissionService rolePermissionService)
    {
        this.rolePermissionService = rolePermissionService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(RolePermissionForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.rolePermissionService.CreateAsync(dto)
        });

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(long id, RolePermissionForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.rolePermissionService.ModifyAsync(id, dto)
        });
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.rolePermissionService.RemoveAsync(id)
        });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.rolePermissionService.RetrieveByIdAsync(id)
        });

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
       => Ok(new
       {
           Code = 200,
           Message = "OK",
           Data = await this.rolePermissionService.RetrieveAllAsync(@params)
       });
}
