using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.IRepositories.Authorizations;
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

public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public RoleService(IMapper mapper,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<RoleForResultDto> AddAsync(RoleForCreationDto dto)
    {
        var exist = await _roleRepository.SelectAll()
            .Where(r => r.Name.ToLower().Equals(dto.Name.ToLower()))
            .FirstOrDefaultAsync();   

        if (exist is not null )
            throw new SwapSpotException(404, "Role is already exist");

        var mapped = _mapper.Map<Role>(dto);
        await _roleRepository.InsertAsync(mapped);
        mapped.CreatedAt = DateTime.UtcNow;

        return _mapper.Map<RoleForResultDto>(mapped);
    }

    public async Task<IEnumerable<RoleForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var roles = await _roleRepository.SelectAll()
        .ToPagedList(@params)
        .ToListAsync();

        return _mapper.Map<IEnumerable<RoleForResultDto>>(roles);
    }

    public async Task<RoleForResultDto> ModifyAsync(RoleForCreationDto dto)
    {
        var exist = await _roleRepository.SelectAll()
            .Where(r => r.Name.ToLower().Equals(dto.Name.ToLower()))
            .FirstOrDefaultAsync();

        if (exist is null)
            throw new SwapSpotException(404, "Role is not found");

        var mapped = _mapper.Map<Role>(dto);

        await _roleRepository.UpdateAsync(mapped);
        mapped.UpdatedAt = DateTime.UtcNow;

        return _mapper.Map<RoleForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var exist = await _roleRepository.SelectAll()
            .Where(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (exist is null)
            throw new SwapSpotException(404, "Role is not found");

        await _roleRepository.DeleteAsync(id);

        return true;
    }

    public async Task<Role> RetrieveByIdForLoginAsync(long id)
    {
        var exist = await _roleRepository.SelectAll()
           .Where(r => r.Id.Equals(id))
           .FirstOrDefaultAsync();

        if (exist is null)
            throw new SwapSpotException(404, "Role is not found");

        return exist;
    }

    public async Task<RoleForResultDto> RetrieveByIdAsync(long id)
    {
        var exist = await _roleRepository.SelectAll()
            .Where(r => r.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (exist is null)
            throw new SwapSpotException(404, "Role is not found");

        return _mapper.Map<RoleForResultDto>(exist);
    }

    public async Task<bool> AssignRoleForUserAsync(long userId, long roleId)
    {
        var existUser = await _userRepository.SelectAll()
            .Where(r => r.Id.Equals(userId))
            .FirstOrDefaultAsync();

        var existRole = await _roleRepository.SelectAll()
            .Where(r => r.Id.Equals(roleId))
            .FirstOrDefaultAsync();
        if ((existRole is null) || ( existUser is null))
            throw new SwapSpotException(404, "User or Role is not found");

        existUser.RoleId = roleId;
        return true;
    }
}
