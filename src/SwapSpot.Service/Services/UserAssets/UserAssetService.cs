using AutoMapper;
using Microsoft.AspNetCore.Http;
using SwapSpot.DAL.IRepositories;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Assets;
using SwapSpot.Service.Interfaces.Assets;
using SwapSpot.Service.Exceptions;
using SwapSpot.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using SwapSpot.Domain.Entities.Assets;
using SwapSpot.Service.Extentions;
using SwapSpot.Service.Helpers;

namespace SwapSpot.Service.Services.UserAssets;

public class UserAssetService : IUserAssetService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserAssetRepository _userAssetRepository;
    private readonly IUserAssetPhotoRepository _userAssetPhotoRepository;
    public UserAssetService(
        IMapper mapper,
        IUserRepository userRepository,
        IUserAssetRepository userAssetRepository,
        IUserAssetPhotoRepository userAssetPhotoRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userAssetRepository = userAssetRepository;
        _userAssetPhotoRepository = userAssetPhotoRepository;
    }
    public async Task<UserAssetForResultDto> AddAsync(UserAssetForCreationDto dto)
    {
        var existUser = await _userRepository.SelectAll()
                    .Where(u => u.Id == dto.UserId)
                    .FirstOrDefaultAsync();

        if (existUser is not null)
            throw new SwapSpotException(409, "User is already exist");

        var existAsset = await _userAssetRepository.SelectAll()
                    .Where(a => a.Name.ToLower() == dto.Name.ToLower() && a.Id == dto.UserId)
                    .FirstOrDefaultAsync();

        if (existAsset is not null)
            throw new SwapSpotException(409, "User Asset is already exist");

        var mappedUserAsset = _mapper.Map<UserAsset>(dto);
        mappedUserAsset.CreatedAt = DateTime.UtcNow;
        var addedModel = await _userAssetRepository.InsertAsync(mappedUserAsset);

        return _mapper.Map<UserAssetForResultDto>(addedModel);
    }

    public async Task<UserAssetForResultDto> ModifyAsync(long userId,long id, UserAssetForUpdateDto dto)
    {
        var user = await _userRepository.SelectAsync(userId);
        if (user is null)
            throw new SwapSpotException(404, "User is not found");

        var userAsset = await _userAssetRepository.SelectAsync(id);
        if (userAsset is null)
            throw new SwapSpotException(404, "UserAsset is not found");

        var modifiedUserAsset = _mapper.Map(dto, userAsset);

        modifiedUserAsset.UpdatedAt = DateTime.UtcNow;

        await _userAssetRepository.UpdateAsync(modifiedUserAsset);

        return _mapper.Map<UserAssetForResultDto>(modifiedUserAsset);
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

    public async Task<IEnumerable<UserAssetForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var userAssets = await _userAssetRepository.SelectAll()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserAssetForResultDto>>(userAssets);
    }

    public async Task<UserAssetForResultDto> RetrieveByIdAsync(long id)
    {
        var userAsset = await _userAssetRepository.SelectAsync(id);
        if (userAsset is null)
            throw new SwapSpotException(404, "UserAsset is not found");

        return _mapper.Map<UserAssetForResultDto>(userAsset);
    }

    public async Task<UserAssetForResultDto> RetrieveByNameAsync(string name)
    {
        var userAsset = await _userAssetRepository.SelectAll()
            .Where(ua => ua.Name.ToLower() == name.ToLower())
            .FirstOrDefaultAsync();
        if (userAsset is null)
            throw new SwapSpotException(404, "UserAsset is not found");

        return _mapper.Map<UserAssetForResultDto>(userAsset);
    }

    public async Task<bool> UploadImage(long id, IFormFile formFile)
    {
        long? userId = HttpContextHelper.UserId;
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new SwapSpotException(404, "User is not found");

        var asset = await _userAssetRepository.SelectAll()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new SwapSpotException(404, "User Asset is not found");

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName);
        var rootPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, "Media", "UserAssetPhotos", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        var mappedAsset = new UserAssetPhoto()
        {
            UserAssetId = id,
            Name = fileName,
            Path = Path.Combine("Media", "UserAssetPhotos", formFile.FileName),
            Extension = Path.GetExtension(formFile.FileName),
            Size = formFile.Length,
            Type = formFile.ContentType,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userAssetPhotoRepository.InsertAsync(mappedAsset);

        return true;
    }
}
