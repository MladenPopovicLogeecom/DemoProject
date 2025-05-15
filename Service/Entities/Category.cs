namespace Service.Entities;

public class Category(string title, string code, string description, Guid? parentCategoryId, DateTime? deletedAt)
{
    public Guid? Id { get; set; }

    public string Title { get; set; } = title;

    public string Code { get; set; } = code; // unique

    public string? Description { get; set; } = description;

    public Guid? ParentCategoryId { get; set; } = parentCategoryId;

    public DateTime? DeletedAt { get; set; } = deletedAt;

    public List<Category> ChildCategories { get; init; } = new();
}