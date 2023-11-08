using AutoMapper;
using SwapSpot.Domain.Authorizations;
using SwapSpot.Domain.Entities.Addresses;
using SwapSpot.Domain.Entities.Assets;
using SwapSpot.Domain.Entities.Users;
using SwapSpot.Service.DTOs.Addresses;
using SwapSpot.Service.DTOs.Assets;
using SwapSpot.Service.DTOs.Authorizations.Roles;
using SwapSpot.Service.DTOs.Users;

namespace SwapSpot.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //Users
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();

        //UserAsset
        CreateMap<UserAsset, UserAssetForCreationDto>().ReverseMap();
        CreateMap<UserAsset, UserAssetForUpdateDto>().ReverseMap();
        CreateMap<UserAsset, UserAssetForResultDto>().ReverseMap();

        //Addressess
        CreateMap<Address, AddressForCreationDto>().ReverseMap();
        CreateMap<Address, AddressForUpdateDto>().ReverseMap();
        CreateMap<Address, AddressForResultDto>().ReverseMap();

        //ROle
        CreateMap<Role, RoleForCreationDto>().ReverseMap();
        CreateMap<Role, RoleForResultDto>().ReverseMap();
    }
}
