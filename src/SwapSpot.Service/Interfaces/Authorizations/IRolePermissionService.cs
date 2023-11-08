using SwapSpot.Domain.Authorizations;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Authorizations;

namespace SwapSpot.Service.Interfaces.Authorizations;

public interface IRolePermissionService
{
    Task<bool> RemoveAsync(long id);
    Task<RolePermissionForResultDto> RetrieveByIdAsync(long id);
    Task<bool> CheckPermission(string role, string accessedMethod);
    Task<IEnumerable<RolePermissionForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<RolePermissionForResultDto> ModifyAsync(long id, RolePermissionForCreationDto dto);
    Task<RolePermissionForResultDto> CreateAsync(RolePermissionForCreationDto dto);
}
