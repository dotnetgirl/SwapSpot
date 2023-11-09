using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwapSpot.Api.Controllers.Commons;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Authorizations.Roles;
using SwapSpot.Service.Interfaces.Authorizations;

namespace SwapSpot.Api.Controllers.Authorizations;

[Authorize(Roles = "Admin, SuperAdmin")]
public class RolesController : BaseController
{
    private readonly IRoleService roleService;

    public RolesController(IRoleService roleService)
    {
        this.roleService = roleService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(RoleForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.roleService.AddAsync(dto)
        });

    [HttpPut]
    public async Task<IActionResult> PutAsync(RoleForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.roleService.ModifyAsync(dto)
        });

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.roleService.RemoveAsync(id)
        });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new
        {
            Code = 200,
            Message = "OK",
            Data = await this.roleService.RetrieveByIdAsync(id)
        });

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
      => Ok(new
      {
          Code = 200,
          Message = "OK",
          Data = await this.roleService.RetrieveAllAsync(@params)
      });

    [HttpPut("{user-id}/{role-id}")]
    public async Task<IActionResult> AssignRoleForUser([FromRoute(Name = "user-id")] long userId, [FromRoute(Name = "role-id")] long roleId)
        => Ok(new
        {
            Code = 200,
            Message = "Ok",
            Data = await this.roleService.AssignRoleForUserAsync(userId, roleId)
        });
}
