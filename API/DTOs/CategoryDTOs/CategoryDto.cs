namespace API.DTOs.CategoryDTOs;

public class CategoryDto
{
    public required string Title { get; set; }
    public required string Code { get; set; }
    public required string? Description { get; set; }

    public Guid? ParentCategoryId { get; init; }
}