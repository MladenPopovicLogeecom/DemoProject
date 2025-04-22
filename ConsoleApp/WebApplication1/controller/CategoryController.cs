using Microsoft.AspNetCore.Mvc;
using WebApplication1.model;
using WebApplication1.model.dto.categoryDto;
using WebApplication1.model.entities;

namespace WebApplication1.controller;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly Database _database;

    //Database je registrovan kao singleton, i ubacen je u dependency Injection kroz Program.cs
    //Ovde hvatamo DI putem konstruktora
    public CategoryController(Database database)
    {
        _database = database;
    }
    
    //Svaki put kada se pozove ova klasa, napravice se novi Database, nemoj ovako da radis
    //public Database Database = new Database();

    [HttpGet("{id}")]
    public Category getCategoryWithId(Guid id)
    {
        return _database.GetCategoryWithId(id);
        
    }
    [HttpGet("getAll")]
    public List<Category> getAllCategories(Guid id)
    {
        return _database.Categories;

    }
    
    [HttpPost("add")]
    public void addCategory(AddCategoryDto addCategoryDto)
    {
        Console.WriteLine("radi");
        Console.WriteLine(addCategoryDto);
        Category NewCategory = Category.GetCategoryFromAddCategoryDto(addCategoryDto);
        _database.AddCategory(NewCategory);
        
    } 
    
    [HttpDelete("delete/{id}")]
    public void deleteCategoryWithId(Guid id)
    {
        _database.DeleteCategoryWithId(id);
    }

    [HttpPut("update")]
    public void UpdateWithId(Category category)
    {
        _database.updateCategory(category);

    }

    
        
     
   
    
}