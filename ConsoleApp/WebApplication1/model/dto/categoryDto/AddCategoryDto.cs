namespace WebApplication1.model.dto.categoryDto;

public class AddCategoryDto
{
    
    public string Title { get; set; }
    public string Code { get; set; } //unikatan
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; } //? je nullable

    public override string ToString()
    {
        return base.ToString();
    }
}