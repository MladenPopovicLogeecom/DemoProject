using Service.Entities;

namespace Service.Contracts.Repository;

public interface IProductRepository: IRepository<Product>
{
    Task Delete(Guid id);
    Task<Product?> GetProductByTitle(string title);
    Task<Product?> GetProductBySku(string sku);
    Task<List<Product>>GetRecentlyViewedProducts();
    Task UnflaggRecentViewProducts();
    
    bool ExistsByCategoryId(Guid categoryId);



}