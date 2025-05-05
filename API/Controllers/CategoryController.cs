using API.DTOs.CategoryDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Entities;
using Service.Exceptions.CategoryExceptions;
using Service.Services.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ICategoryService categoryService;

    
    public CategoryController(ICategoryService iCategoryService, IMapper iMapper)
    {
        mapper = iMapper;
        categoryService = iCategoryService;

       
    }

    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryWithId(Guid id)
    {
        try
        {
            return Ok(categoryService.GetById(id));
        }
        catch (CategoryWithIdNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpGet]
    public List<Category> GetAllCategories()
    {
        return categoryService.GetAll();
    }    
    
    //// Useful for building hierarchical category views where child categories are shown under each parent.
    [HttpGet("parents")]
    public List<Category> GetAllParents()
    {
        return categoryService.GetAllParents();
    }

    [HttpPost]
    public ActionResult AddCategory(CategoryDto categoryDto)
    {
        Category newCategory = mapper.Map<Category>(categoryDto);

        try
        {
            categoryService.Add(newCategory);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok("Category Created");
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(Guid id)
    {
        try
        {
            categoryService.DeleteById(id);
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
    public ActionResult UpdateById(Guid id, Category dto)
    {
        try
        {
            categoryService.Update(id, dto);
        }
        catch (CategoryWithIdNotFoundException categoryWithIdNotFoundException)
        {
            return NotFound(categoryWithIdNotFoundException.Message);
        }

        return Ok("Category updated!");
    }
}