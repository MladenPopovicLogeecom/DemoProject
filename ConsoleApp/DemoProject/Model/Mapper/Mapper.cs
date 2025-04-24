using AutoMapper;
using DemoProject.model.dto.categoryDto;
using DemoProject.model.entities;

namespace DemoProject.model.mapper;

//Will be used in the future!
public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<CategoryDto, Category>();
    }
}