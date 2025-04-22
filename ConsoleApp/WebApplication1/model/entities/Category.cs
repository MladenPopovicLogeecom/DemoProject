using WebApplication1.model.dto.categoryDto;

namespace WebApplication1.model.entities;

public class Category
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Code { get; set; } //unikatan
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; } //? je nullable
    
    public Category(string title, string code, string description, Guid? parentCategoryId)
    {
        Title = title;
        Code = code;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }
    public override string ToString()
    {
        return $"Category [Id: {Id}, Title: {Title}, Code: {Code}, Description: {Description}]";
    }

    public static Category getCategoryFromAddCategoryDto(AddCategoryDto dto)
    {
        Category NewCat = new Category(dto.Title, dto.Code, dto.Description, dto.ParentCategoryId);
        NewCat.Id = Guid.NewGuid();
        return NewCat;

    }
}