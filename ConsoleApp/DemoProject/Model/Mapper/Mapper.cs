using AutoMapper;
using WebApplication1.model.dto.categoryDto;
using WebApplication1.model.entities;

namespace WebApplication1.model.mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<AddCategoryDto, Category>();
    }
}