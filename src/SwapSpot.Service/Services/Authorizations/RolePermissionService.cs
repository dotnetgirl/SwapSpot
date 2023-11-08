using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwapSpot.DAL.IRepositories.Authorizations;
using SwapSpot.DAL.IRepositories.Commons;
using SwapSpot.DAL.Repositories.Authorizations;
using SwapSpot.Domain.Authorizations;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Authorizations;
using SwapSpot.Service.DTOs.Authorizations.Roles;
using SwapSpot.Service.Exceptions;
using SwapSpot.Service.Extentions;
using SwapSpot.Service.Interfaces.Authorizations;

namespace SwapSpot.Service.Services.Authorizations;

public class RolePermissionService : IRolePermissionService
{
    private readonly IMapper _mapper;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    public RolePermissionService(IMapper mapper, IRolePermissionRepository rolePermissionRepository)
    {
        _mapper = mapper;
        _rolePermissionRepository = rolePermissionRepository;
    }
    public async Task<bool> CheckPermission(string role, string accessedMethod)
    {
        var permissions = await _rolePermissionRepository
                 .SelectAll()
                 .Where(p => p.Role.Name.ToLower() == role.ToLower())
                 .ToListAsync();
        foreach (var permission in permissions)
        {
            if (permission?.Permisson?.Name.ToLower() == accessedMethod.ToLower())
                return true;
        }

        return false;
    }

    public async Task<RolePermissionForResultDto> CreateAsync(RolePermissionForCreationDto dto)
    {
        var exist = await _rolePermissionRepository.SelectAll()
             .Where(rp => rp.RoleId.Equals(dto.RoleId) && rp.PermissonId.Equals(dto.PermissonId))
             .FirstOrDefaultAsync();

        if (exist is not null)
            throw new SwapSpotException(409, "RolePermission is already exist");

        var mappedRolePermission = _mapper.Map<RolePermission>(dto);

        var result = await _rolePermissionRepository.InsertAsync(mappedRolePermission);
        result.CreatedAt = DateTime.UtcNow;

        return _mapper.Map<RolePermissionForResultDto>(result);
    }

    public async Task<RolePermissionForResultDto> ModifyAsync(long id, RolePermissionForCreationDto dto)
    {
        var exist = await _rolePermissionRepository.SelectAsync(id);

        if (exist is null)
            throw new SwapSpotException(404, "RolePermission is not exist");

        var mappedRolePermission = _mapper.Map<RolePermission>(dto);

        await _rolePermissionRepository.UpdateAsync(mappedRolePermission);

        return _mapper.Map<RolePermissionForResultDto>(mappedRolePermission);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var exist = await _rolePermissionRepository.SelectAll()
            .Where(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (exist is null)
            throw new SwapSpotException(404, "RolePermission is not found");

        await _rolePermissionRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<RolePermissionForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var rolePermissions = await _rolePermissionRepository.SelectAll()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<RolePermissionForResultDto>>(rolePermissions);
    }

    public async Task<RolePermissionForResultDto> RetrieveByIdAsync(long id)
    {
        var exist = await _rolePermissionRepository.SelectAll()
            .Where(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (exist is null)
            throw new SwapSpotException(404, "RolePermission is not found");

        return _mapper.Map<RolePermissionForResultDto>(exist);
    }
}
