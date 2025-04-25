using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Dto.CategoryDto;

public class CategoryDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Code is required.")]
    [StringLength(50, ErrorMessage = "Code cannot be longer than 50 characters.")]
    public string Code { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string Description { get; set; }

    public Guid? ParentCategoryId { get; set; }
}