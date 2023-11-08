using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwapSpot.DAL.IRepositories.Authorizations;
using SwapSpot.DAL.IRepositories.Commons;
using SwapSpot.DAL.Repositories.Authorizations;
using SwapSpot.Domain.Authorizations;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Authorizations.Permissions;
using SwapSpot.Service.DTOs.Authorizations.Roles;
using SwapSpot.Service.Exceptions;
using SwapSpot.Service.Extentions;
using SwapSpot.Service.Interfaces.Authorizations;
using System.Data;

namespace SwapSpot.Service.Services.Authorizations;

public class PermissionService : IPermissionService
{
    private readonly IMapper _mapper;
    private readonly IPermissionRepository _permissionRepository;
    public PermissionService(IMapper mapper, IPermissionRepository permissionRepository)
    {
        _mapper = mapper;
        _permissionRepository = permissionRepository;
    }

    public async Task<PermissionForResultDto> CreateAsync(PermissionForCreationDto dto)
    {
        var exist = await _permissionRepository.SelectAll()
            .Where(p => p.Name.Equals(dto.Name))
            .FirstOrDefaultAsync();
        if (exist is not null || !exist.IsDeleted)
            throw new SwapSpotException(404, "Permission is already exist");

        var mappedPermission = _mapper.Map<Permission>(dto);
        await _permissionRepository.InsertAsync(mappedPermission);

        return _mapper.Map<PermissionForResultDto>(dto);
    }

    public async Task<IEnumerable<PermissionForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var permissions = await _permissionRepository.SelectAll()
            .Where(p => p.IsDeleted == false)
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PermissionForResultDto>>(permissions);
    }

    public async Task<PermissionForResultDto> RetrieveByIdAsync(long id)
    {
        var exist = await _permissionRepository.SelectAll()
            .Where(p => p.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (exist is null || !exist.IsDeleted)
            throw new SwapSpotException(404, "Permission is not found");

        return _mapper.Map<PermissionForResultDto>(exist);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var exist = await _permissionRepository.SelectAll()
            .Where(p => p.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (exist is null || !exist.IsDeleted)
            throw new SwapSpotException(404, "Permission is not found");

        await _permissionRepository.DeleteAsync(id);

        return true;
    }

    public async Task<PermissionForResultDto> ModifyAsync(PermissionForCreationDto dto)
    {
        var exist = await _permissionRepository.SelectAll()
            .Where(p => p.Name.ToLower().Equals(dto.Name.ToLower()))
            .FirstOrDefaultAsync();

        if (exist is null || !exist.IsDeleted)
            throw new SwapSpotException(404, "Permission is not found");

        var mapped = _mapper.Map<Permission>(dto);

        await _permissionRepository.UpdateAsync(mapped);
        mapped.UpdatedAt = DateTime.UtcNow;

        return _mapper.Map<PermissionForResultDto>(mapped);
    }
}
