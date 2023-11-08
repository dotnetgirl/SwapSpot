using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.Repositories.Authorizations;
using SwapSpot.Domain.Entities.Addresses;
using SwapSpot.Domain.Entities.Users;
using SwapSpot.Service.Configurations;
using SwapSpot.Service.DTOs.Addresses;
using SwapSpot.Service.DTOs.Authorizations.Permissions;
using SwapSpot.Service.Exceptions;
using SwapSpot.Service.Extentions;
using SwapSpot.Service.Interfaces.Addresses;
using System.Threading.Channels;

namespace SwapSpot.Service.Services.Addresses;

public class AddressService : IAddressService
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;
    private readonly IUserRepository _userRepository;

    public AddressService(IAddressRepository addressRepository, IMapper mapper, IUserRepository userRepository)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<AddressForResultDto> AddAsync(long userId, AddressForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
             .Where(u => u.Id == userId)
             .FirstOrDefaultAsync();

        if (user is null)
            throw new SwapSpotException(404, "User is not found!");

        var address = await _addressRepository.SelectAll()
             .Where(u => u.Home == dto.Home)
             .FirstOrDefaultAsync();

        if (user is not null)
            throw new SwapSpotException(400, "Address is already exist");

        var mapped = _mapper.Map<Address>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.UserId = userId;

        var result = await _addressRepository.InsertAsync(mapped);

        return _mapper.Map<AddressForResultDto>(result);
    }

    public async Task<AddressForResultDto> UpdateByIdAsync(long userId, long id, AddressForUpdateDto dto)
    {
        var address = await _addressRepository.SelectAll()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();

        if (address is null)
            throw new SwapSpotException(404, "Address is not found!");

        var mapped = _mapper.Map(dto, address);
        mapped.UpdatedAt = DateTime.UtcNow;
        mapped.UserId = userId;

        await _addressRepository.UpdateAsync(mapped);

        return _mapper.Map<AddressForResultDto>(mapped);
    }
    public async Task<bool> DeleteByIdAsync(long id)
    {
        var address = await _addressRepository.SelectAsync(id);

        if (address is null)
            throw new SwapSpotException(404, "Address is not found!");

        await _addressRepository.DeleteAsync(id);
        return true;
    }

    public async Task<AddressForResultDto> GetByIdAsync(long userId, long id)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            throw new SwapSpotException(404, "User is not found!");

        var address = await _addressRepository.SelectAll()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();

        if (address is null)
            throw new SwapSpotException(404, "Address is not found!");

        return _mapper.Map<AddressForResultDto>(address);
    }

    public async Task<IEnumerable<AddressForResultDto>> GetAllAsync(PaginationParams @params)
    {
        var addresses = await _addressRepository.SelectAll()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AddressForResultDto>>(addresses);
    }
}
