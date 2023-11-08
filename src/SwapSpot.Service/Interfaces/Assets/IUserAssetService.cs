using Microsoft.AspNetCore.Http;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Assets;

namespace SwapSpot.Service.Interfaces.Assets;

public interface IUserAssetService 
{
    Task<UserAssetForResultDto> AddAsync(UserAssetForCreationDto dto);
    Task<IEnumerable<UserAssetForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<bool> RemoveAsync(long id);
    Task<UserAssetForResultDto> RetrieveByIdAsync(long id);
    Task<UserAssetForResultDto> RetrieveByNameAsync(string name);
    Task<UserAssetForResultDto> ModifyAsync(long userId, long id, UserAssetForUpdateDto dto);
    Task<bool> UploadImage(long id, IFormFile formFile);
}