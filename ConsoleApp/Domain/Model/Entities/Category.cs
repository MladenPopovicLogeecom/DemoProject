namespace Domain.Model.Entities;


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

    public string Title { get; set; }

    public string Code { get; set; } // unique

    public string Description { get; set; }

    public Guid? ParentCategoryId { get; set; } //? is nullable

    public List<Category> ChildCategories { get; set; }
}