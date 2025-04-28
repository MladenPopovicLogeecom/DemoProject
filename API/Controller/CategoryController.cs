using AutoMapper;
using Domain.Model.Dto.CategoryDto;
using Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.MyExceptions.CategoryExceptions;
using Service.Service.Implementation;

namespace API.Controller;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly IMapper mapper;
    private static CategoryService CategoryService;

    
    public CategoryController()
    {
        //Mapper = new AutoMapper.Mapper();
        if (CategoryService == null)
        {
            CategoryService = new CategoryService();
            CategoryService.seedDatabase();
            
        }
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Mapper.Mapper>();
        });
        mapper = config.CreateMapper();  
    }

    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryWithId(Guid id)
    {
        try
        {
            var category = CategoryService.GetCategoryWithId(id);
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
        var newCategory = mapper.Map<Category>(categoryDto);

        try
        {
            CategoryService.AddCategory(newCategory);
        }
        catch (Exception exception)
        {
            return Conflict(exception.Message);
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
            return Conflict(exception.Message);
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