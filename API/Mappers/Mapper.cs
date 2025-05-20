using API.DTOs.CategoryDTOs;
using API.DTOs.ProductDTOs;
using AutoMapper;
using Service.Entities;

namespace API.Mappers;


public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<CategoryDto, Category>();
        CreateMap<ProductDto, Product>();
    }
}