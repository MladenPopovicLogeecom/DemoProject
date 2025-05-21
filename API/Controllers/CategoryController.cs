using API.DTOs.CategoryDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Entities;
using Service.Exceptions.CategoryExceptions;
using Service.Services.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("category")]
public class CategoryController(ICategoryService categoryService, IMapper mapper) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryWithId(Guid id)
    {
        try
        {
            return Ok(await categoryService.GetById(id));
        }
        catch (CategoryWithIdNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpGet]
    public async Task<List<Category>> GetAllCategories()
    {
        return await categoryService.GetAll();
    }

    //// Useful for building hierarchical category views where child categories are
    /// shown under each parent.
    [HttpGet("parents")]
    public async Task<List<Category>> GetAllParents()
    {
        return await categoryService.GetAllParents();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> AddCategory(CategoryDto categoryDto)
    {
        Category newCategory = mapper.Map<Category>(categoryDto);

        try
        {
            await categoryService.Add(newCategory);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok("Category Created");
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteById(Guid id)
    {
        try
        {
            await categoryService.SoftDelete(id);
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

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateById(Guid id, CategoryDto dto)
    {
        Category newCategory = mapper.Map<Category>(dto);
        try
        {
            await categoryService.Update(id, newCategory);
        }
        catch (CategoryWithIdNotFoundException categoryWithIdNotFoundException)
        {
            return NotFound(categoryWithIdNotFoundException.Message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok("Category updated!");
    }
}