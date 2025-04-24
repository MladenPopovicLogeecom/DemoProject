using System.ComponentModel.DataAnnotations;

namespace DemoProject.model.entities;

public class Category
{
    public Category(string title, string code, string description, Guid? parentCategoryId)
    {
        Title = title;
        Code = code;
        Description = description;
        ParentCategoryId = parentCategoryId;
        ChildCategories = new List<Category>();
    }

    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(20, ErrorMessage = "Title cannot be longer than 20 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Code is required.")]
    [StringLength(50, ErrorMessage = "Code cannot be longer than 50 characters.")]
    public string Code { get; set; } // unique

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string Description { get; set; }

    public Guid? ParentCategoryId { get; set; } //? is nullable

    public List<Category> ChildCategories { get; set; }
}