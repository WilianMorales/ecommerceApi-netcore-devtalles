using System;
using AutoMapper;
using ecommerceApi_netcore_devtalles.Models;
using ecommerceApi_netcore_devtalles.Models.Dtos;

namespace ecommerceApi_netcore_devtalles.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
    }
}
