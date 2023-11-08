using Microsoft.AspNetCore.Http;
using SwapSpot.Domain.Entities.Users;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Users;

namespace SwapSpot.Service.Interfaces.Users;

public interface IUserService
{
    Task<UserForResultDto> AddAsync(UserForCreationDto dto);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<User> RetrieveByEmailAsync(string email);
    Task<bool> RemoveAsync(long id);
    Task<UserForResultDto> RetrieveByIdAsync(long id);
    Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto);
    Task<UserForResultDto> ChangePasswordAsync(UserForUpdatePasswordDto dto);
    Task<bool> UploadImage(IFormFile formFile);
}