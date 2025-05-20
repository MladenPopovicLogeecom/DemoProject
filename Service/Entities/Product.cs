namespace Service.Entities;

public class Product
{
    public Product() { }

    public Product(Guid? id, string sku, string title, string brand, Guid categoryId, decimal price, string? shortDescription, string? longDescription, bool isEnabled, bool isFeatured, int viewCount, DateTime visitedAt)
    {
        Id = id;
        Sku = sku;
        Title = title;
        Brand = brand;
        CategoryId = categoryId;
        Price = price;
        ShortDescription = shortDescription;
        LongDescription = longDescription;
        IsEnabled = isEnabled;
        IsFeatured = isFeatured;
        ViewCount = viewCount;
        VisitedAt = visitedAt;
    }

    public Guid? Id { get; init; }
    public required string Sku { get; set; }
    public required string Title { get; set; }
    public required string Brand { get; set; }
    public required Guid CategoryId { get; init; }
    public required decimal Price { get; set; }
    public string? ShortDescription { get; set; }
    public string? LongDescription { get; set; }
    public bool IsEnabled { get; set; }
    public bool IsFeatured { get; set; }
    public int ViewCount { get; set; }
    public DateTime? VisitedAt { get; set; }
}