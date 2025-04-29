namespace PresentationLayer.Entities;


public class Category(string title, string code, string description, Guid? parentCategoryId)
{
    public Guid? Id { get; set; }

    public string Title { get; set; } = title;

    public string Code { get; set; } = code; // unique

    public string? Description { get; set; } = description;

    public Guid? ParentCategoryId { get; set; } = parentCategoryId; //? is nullable

    public List<Category> ChildCategories { get; init; } = new();
}