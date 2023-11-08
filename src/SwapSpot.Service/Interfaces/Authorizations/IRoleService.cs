using SwapSpot.Domain.Authorizations;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Authorizations.Roles;

namespace SwapSpot.Service.Interfaces.Authorizations;

public interface IRoleService
{
    Task<bool> RemoveAsync(long id);
    Task<Role> RetrieveByIdForLoginAsync(long id);
    Task<RoleForResultDto> RetrieveByIdAsync(long id);
    Task<RoleForResultDto> ModifyAsync (RoleForCreationDto dto);
    Task<RoleForResultDto> AddAsync(RoleForCreationDto dto);
    Task<bool> AssignRoleForUserAsync(long userId, long roleId);
    Task<IEnumerable<RoleForResultDto>> RetrieveAllAsync(PaginationParams @params);
}