using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.IRepositories.Authorizations;
using SwapSpot.Domain.Entities.Users;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Users;
using SwapSpot.Service.Exceptions;
using SwapSpot.Service.Extentions;
using SwapSpot.Service.Helpers;
using SwapSpot.Service.Interfaces.Users;
using SwapSpot.Shared.Helpers;

namespace SwapSpot.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserPhotoRepository _userPhotoRepository;
    public UserService(
        IMapper mapper,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserPhotoRepository userPhotoRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userPhotoRepository = userPhotoRepository;
    }
    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var existUser = await _userRepository.SelectAll()
                    .Where(u => u.Phone == dto.Phone)
                    .FirstOrDefaultAsync();
        if (existUser is not null)
            throw new SwapSpotException(409, "User is already exist");

        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.CreatedAt = DateTime.UtcNow;
        mappedUser.Password = PasswordHelper.Hash(dto.Password);
        mappedUser.RoleId = 3;
        var addedModel = await _userRepository.InsertAsync(mappedUser);

        return _mapper.Map<UserForResultDto>(addedModel);
    }
    public async Task<bool> RemoveAsync(long id)
    {
        var user = await _userRepository.SelectAll()
                        .Where(u => u.Id == id)
                        .FirstOrDefaultAsync();
        if (user is null)
            throw new SwapSpotException(404, "User is not found");

        await _userRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var users = _userRepository.SelectAll();
        if (!users.Any())
            return new List<UserForResultDto>();

        var result = await users.ToPagedList(@params).ToListAsync();
        return _mapper.Map<IEnumerable<UserForResultDto>>(result);
    }
    public async Task<IEnumerable<UserForResultDto>> RetrieveAllByRoleAsync(PaginationParams @params, long roleId)
    {
        var users = await _userRepository.SelectAll()
            .Where(u => u.RoleId == roleId)
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(users);

    }
    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var user = await _userRepository.SelectAsync(id);
        if (user is null)
            throw new SwapSpotException(404, "User is not found");

        return _mapper.Map<UserForResultDto>(user);
    }
    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var user = await _userRepository.SelectAsync(id);
        if (user is null)
            throw new SwapSpotException(404, "User is not found");

        var modifiedUser = _mapper.Map(dto, user);

        modifiedUser.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(modifiedUser);

        return _mapper.Map<UserForResultDto>(user);
    }
    public async Task<User> RetrieveByEmailAsync(string email)
    {
        var user = await _userRepository.SelectAll()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

        return user;
    }
    public async Task<UserForResultDto> ChangePasswordAsync(UserForUpdatePasswordDto dto)
    {
        var user = await _userRepository.SelectAll()
                .Where(u => u.Email == dto.Email)
                .FirstOrDefaultAsync();

        if (user is null)
            throw new SwapSpotException(404, "User is not found");

        if (!PasswordHelper.Verify(dto.OldPassword, user.Password))
            throw new SwapSpotException(400, "Password is incorrect");

        if (dto.NewPassword != dto.ComfirmPassword)
            throw new SwapSpotException(400, "New password and confirm password are not equal");

        user.Password = PasswordHelper.Hash(dto.NewPassword);

        return _mapper.Map<UserForResultDto>(user);
    }

    public async Task<bool> UploadImage(IFormFile formFile)
    {
        long? userId = HttpContextHelper.UserId;
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new SwapSpotException(404, "User is not found");

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName);
        var rootPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, "Media", "UserPhotos", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        var mappedAsset = new UserPhoto()
        {
            UserId = userId,
            Name = fileName,
            Path = Path.Combine("Media", "UserPhotos", formFile.FileName),
            Extension = Path.GetExtension(formFile.FileName),
            Size = formFile.Length,
            Type = formFile.ContentType,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userPhotoRepository.InsertAsync(mappedAsset);

        return true;
    }
}
