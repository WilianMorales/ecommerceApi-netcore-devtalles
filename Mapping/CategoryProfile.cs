using System;
using ecommerceApi_netcore_devtalles.Models.Dtos;
using AutoMapper;

namespace ecommerceApi_netcore_devtalles.Mapping;

public class CategoryProfile: Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
    }
}
