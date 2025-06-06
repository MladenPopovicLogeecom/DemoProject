﻿using Service.BusinessValidators;
using Service.Contracts.Repository;
using Service.Entities;
using Service.Services.Interfaces;

namespace Service.Services.Implementation;

public class ProductService(
    IProductRepository productRepository,
    ProductBusinessValidator productBusinessValidator,
    CategoryBusinessValidator categoryBusinessValidator)
    : IProductService
{
    public async Task Update(Guid id, Product updatedProduct)
    {
        Product product = await productBusinessValidator.EnsureProductExists(id);

        if (product.Sku != updatedProduct.Sku)
        {
            await productBusinessValidator.EnsureSkuIsUnique(updatedProduct.Sku);
        }

        if (product.Title != updatedProduct.Title)
        {
            await productBusinessValidator.EnsureTitleIsUnique(updatedProduct.Title);
        }

        await categoryBusinessValidator.EnsureCategoryExists(updatedProduct.CategoryId);

        product.ApplyUpdatesFrom(updatedProduct);

        await productRepository.Update(product);
    }

    public async Task<List<Product>> GetAll()
    {
        return await productRepository.GetAll();
    }

    public async Task<Product> GetById(Guid id)
    {
        Product product = await productBusinessValidator.EnsureProductExists(id);

        product.VisitedAt = DateTime.UtcNow;
        product.ViewCount += 1;

        await productRepository.Update(product);

        return product;
    }

    public async Task Add(Product product)
    {
        await productBusinessValidator.EnsureSkuIsUnique(product.Sku);
        await productBusinessValidator.EnsureTitleIsUnique(product.Title);
        await categoryBusinessValidator.EnsureCategoryExists(product.CategoryId);

        await productRepository.Add(product);
    }

    public async Task Delete(Guid id)
    {
        await productBusinessValidator.EnsureProductExists(id);
        await productRepository.Delete(id);
    }

    public async Task<List<Product>> GetRecentlyViewedProducts()
    {
        return await productRepository.GetRecentlyViewedProducts();
    }
}