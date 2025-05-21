namespace API.DTOs.ProductDTOs;

public class ProductDto
{
    public required string Sku { get; init; }
    public required string Title { get; init; }
    public required string Brand { get; init; }
    public required Guid CategoryId { get; init; }
    public required decimal Price { get; init; }
    public string? ShortDescription { get; init; }
    public string? LongDescription { get; init; }
    public bool IsEnabled { get; init; }
    public bool IsFeatured { get; init; }
}