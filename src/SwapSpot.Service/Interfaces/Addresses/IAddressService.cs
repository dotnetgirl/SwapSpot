using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Addresses;

namespace SwapSpot.Service.Interfaces.Addresses;

public interface IAddressService
{
    Task<AddressForResultDto> AddAsync(long userId, AddressForCreationDto address);
    Task<bool> DeleteByIdAsync(long id);
    Task<AddressForResultDto> UpdateByIdAsync(long userId, long id, AddressForUpdateDto dto);
    Task<AddressForResultDto> GetByIdAsync(long userId, long id);
    Task<IEnumerable<AddressForResultDto>> GetAllAsync(PaginationParams @params);
}