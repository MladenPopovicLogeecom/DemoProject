namespace WebApplication1.model.entities;

public class Product
{
    public Guid Id { get; set; }
    public string SKU { get; set; }
    public string Title { get; set; }
    public string Brand { get; set; }
    public Guid CategoryId { get; set; }
    public int Price { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public bool IsEnabled { get; set; }
    public bool isFeatured { get; set; }
    public int ViewCount { get; set; }
    
}