using AutoMapper;
using Domain.Model.Dto.CategoryDto;
using Domain.Model.Entities;

namespace API.Mapper;


public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<CategoryDto, Category>();
    }
}