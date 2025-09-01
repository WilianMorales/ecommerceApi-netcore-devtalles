using System;
using AutoMapper;
using ecommerceApi_netcore_devtalles.Models;
using ecommerceApi_netcore_devtalles.Models.Dtos;

namespace ecommerceApi_netcore_devtalles.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, UserLoginDto>().ReverseMap();
        CreateMap<User, UserLoginResponseDto>().ReverseMap();
    }
}
