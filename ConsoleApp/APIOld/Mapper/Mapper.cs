using AutoMapper;
using Domain.Model.Dto.CategoryDto;
using Domain.Model.Entities;

namespace DemoProject.model.mapper;


public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<CategoryDto, Category>();
    }
}