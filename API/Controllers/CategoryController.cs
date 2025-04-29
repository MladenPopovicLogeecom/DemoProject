using API.DTOs.CategoryDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Entities;
using Service.Exceptions.CategoryExceptions;
using Service.Services.Implementation;
using Mapper = API.Mappers.Mapper;

namespace API.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly IMapper mapper;
    private static CategoryService CategoryService;

    
    public CategoryController()
    {
        
        if (CategoryService == null)
        {
            CategoryService = new CategoryService();
            CategoryService.SeedDatabase();
            
        }
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Mapper>();
        });
        mapper = config.CreateMapper();  
    }

    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryWithId(Guid id)
    {
        try
        {
            Category category = CategoryService.GetCategoryWithId(id);
            return Ok(category);
        }
        catch (CategoryWithIdNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpGet]
    public List<Category> GetAllCategories()
    {
        return CategoryService.GetAllCategories();
    }    
    
    //// Useful for building hierarchical category views where child categories are shown under each parent.
    [HttpGet("parents")]
    public List<Category> GetAllParents()
    {
        return CategoryService.GetAllParents();
    }

    [HttpPost]
    public ActionResult AddCategory(CategoryDto categoryDto)
    {
        Category newCategory = mapper.Map<Category>(categoryDto);

        try
        {
            CategoryService.AddCategory(newCategory);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok("Category Created");
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCategoryWithId(Guid id)
    {
        try
        {
            CategoryService.DeleteCategoryWithId(id);
        }
        catch (CategoryWithIdNotFoundException categoryWithIdNotFoundException)
        {
            return NotFound(categoryWithIdNotFoundException.Message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok("Category Deleted");
    }

    [HttpPut("{id}")]
    public ActionResult UpdateWithId(Guid id, Category dto)
    {
        try
        {
            CategoryService.UpdateCategory(id, dto);
        }
        catch (CategoryWithIdNotFoundException categoryWithIdNotFoundException)
        {
            return NotFound(categoryWithIdNotFoundException.Message);
        }

        return Ok("Category updated!");
    }
}