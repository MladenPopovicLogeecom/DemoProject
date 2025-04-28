
namespace Domain.Model.Dto.CategoryDto;

public class CategoryDto
{
    public string Title { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}