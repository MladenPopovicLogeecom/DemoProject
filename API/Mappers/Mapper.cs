using API.DTOs.CategoryDTOs;
using AutoMapper;
using PresentationLayer.Entities;

namespace API.Mappers;


public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<CategoryDto, Category>();
    }
}