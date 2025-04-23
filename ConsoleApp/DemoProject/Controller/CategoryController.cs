using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.model;
using WebApplication1.model.dto.categoryDto;
using WebApplication1.model.entities;
using WebApplication1.Service;

namespace WebApplication1.controller;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService CategoryService;
    private readonly IMapper Mapper;
    

    public CategoryController(CategoryService categoryService, IMapper mapper)
    {
        Mapper = mapper;
        CategoryService = categoryService;
        
    }
    
    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryWithId(Guid id)
    {
        try
        {
            var category = CategoryService.GetCategoryWithId(id);
            return Ok(category);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    public List<Category> GetAllCategories()
    {
        return CategoryService.GetAllCategories();
    }

    [HttpPost]
    public ActionResult AddCategory(AddCategoryDto addCategoryDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ErrorMessages.GetModelStateErrors(ModelState);
            return BadRequest(errorMessages);
        }
        
        var newCategory = Mapper.Map<Category>(addCategoryDto);
        
        try
        {
            CategoryService.AddCategory(newCategory);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
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
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e2)
        {
            return Conflict(e2.Message);
        }
        
        return Ok("Category Deleted");
    }

    [HttpPut]
    public ActionResult UpdateWithId(Category category)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ErrorMessages.GetModelStateErrors(ModelState);
            return BadRequest(errorMessages);
        }
        try
        {
            CategoryService.UpdateCategory(category);
        }
        catch (NotFoundException e1)
        {
            return NotFound(e1.Message);
        }

        return Ok("Category updated!");
    }
}