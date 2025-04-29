namespace API.DTOs.CategoryDTOs;

public class CategoryDto
{
    public required string Title { get; init; }
    public required string Code { get; init; }
    public string? Description { get; init; }
    public Guid? ParentCategoryId { get; init; }
}