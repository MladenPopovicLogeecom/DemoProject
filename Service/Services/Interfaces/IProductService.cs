using Service.Entities;

namespace Service.Services.Interfaces;

public interface IProductService : IService<Product>
{
    Task Delete(Guid id);
    Task<List<Product>> GetRecentlyViewedProducts();
}