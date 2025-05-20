using Service.BusinessValidators;
using Service.Contracts.Repository;
using Service.Entities;
using Service.Services.Interfaces;

namespace Service.Services.Implementation;

public class ProductService(IProductRepository productRepository,ProductBusinessValidator productBusinessValidator) : IProductService
{
    public async Task Update(Guid id, Product updatedProduct)
    {
        Product product = (await productBusinessValidator.EnsureIdExists(id));

        await productBusinessValidator.EnsureSkuIsUnique(updatedProduct.Sku);
        await productBusinessValidator.EnsureTitleIsUnique(updatedProduct.Title);
        
        product.Sku = updatedProduct.Sku;
        product.Title = updatedProduct.Title;
        product.Brand = updatedProduct.Brand;
        product.Price = updatedProduct.Price;
        product.ShortDescription = updatedProduct.ShortDescription;
        product.LongDescription = updatedProduct.LongDescription;
        product.IsEnabled = updatedProduct.IsEnabled;
        product.IsFeatured = updatedProduct.IsFeatured;

        await productRepository.Update(product);
    }

    public async Task<List<Product>> GetAll()
    {
        return await productRepository.GetAll();
    }

    public async Task<Product> GetById(Guid id)
    {
        Product product=await productBusinessValidator.EnsureIdExists(id);
        product.VisitedAt = DateTime.UtcNow;
        product.ViewCount += 1;
        await productRepository.Update(product);
        return product;
    }
    public async Task Add(Product product)
    {
        await productBusinessValidator.EnsureSkuIsUnique(product.Sku);
        await productBusinessValidator.EnsureTitleIsUnique(product.Title);
        await productRepository.Add(product);
    }

    public async Task Delete(Guid id)
    {
        await productRepository.Delete(id);
    }

    public async Task<List<Product>> GetRecentlyViewedProducts()
    {
        return await productRepository.GetRecentlyViewedProducts();
        
    }
}