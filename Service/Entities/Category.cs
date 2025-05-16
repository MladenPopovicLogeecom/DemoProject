namespace Service.Entities;

public class Category
{
    //We need empty constructor because of AutoMapper
    public Category() { }

    public Category(string title, string code, string description, Guid? parentCategoryId, DateTime? deletedAt)
    {
        Title = title;
        Code = code;
        Description = description;
        ParentCategoryId = parentCategoryId;
        DeletedAt = deletedAt;
    }

    public Guid? Id { get; set; }

    public string Title { get; set; }

    public string Code { get; set; }

    public string? Description { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public DateTime? DeletedAt { get; set; }

    public List<Category> ChildCategories { get; init; } = new();
}