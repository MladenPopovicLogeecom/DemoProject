using API.DTOs.CategoryDTOs;
using AutoMapper;
using Service.Entities;

namespace API.Mappers;


public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<CategoryDto, Category>();
    }
}