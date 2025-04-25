using AutoMapper;
using Service.Service.Interface;
using DemoProject.Exceptions;
using Domain.Model.Dto.CategoryDto;
using Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.controller;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService CategoryService;
    private readonly IMapper Mapper;


    public CategoryController(ICategoryService categoryService, IMapper mapper)
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
    public ActionResult AddCategory(CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var newCategory = Mapper.Map<Category>(categoryDto);

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

    [HttpPut("{id}")]
    public ActionResult UpdateWithId(Guid id, Category dto)
    {
        //TODO
        //change to fluent validation.
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            CategoryService.UpdateCategory(id, dto);
        }
        catch (NotFoundException e1)
        {
            return NotFound(e1.Message);
        }

        return Ok("Category updated!");
    }
}