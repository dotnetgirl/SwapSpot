using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Authorizations.Permissions;

namespace SwapSpot.Service.Interfaces.Authorizations;

public interface IPermissionService
{
    Task<bool> RemoveAsync(long id);
    Task<PermissionForResultDto> RetrieveByIdAsync(long id);
    Task<PermissionForResultDto> ModifyAsync(PermissionForCreationDto dto);
    Task<PermissionForResultDto> CreateAsync(PermissionForCreationDto dto);
    Task<IEnumerable<PermissionForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
