using DataEF.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Service.Contracts.Repository;
using Service.Entities;

namespace DataEF.Repositories;

public class ProductRepositoryPostgre(ApplicationDbContext context) : IProductRepository
{
    public async Task<List<Product>> GetAll()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task Add(Product entity)
    {
        context.Products.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Product entity)
    {
        context.Products.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        Product? product = await context.Products.FindAsync(id);
        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }

    public async Task<Product?> GetProductByTitle(string title)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Title == title);
    }

    public async Task<Product?> GetProductBySku(string sku)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Sku == sku);
    }

    public async Task<List<Product>> GetRecentlyViewedProducts()
    {
        return await context.Products.Where(c => c.VisitedAt != null)
            .OrderByDescending(c => c.VisitedAt).ToListAsync();
    }

    public async Task UnflaggRecentViewProducts()
    {
        List<Product> productsWithVisitedAt = await context.Products
            .Where(p => p.VisitedAt != null)
            .ToListAsync();


        foreach (var product in productsWithVisitedAt)
        {
            product.VisitedAt = null;
            //context.Entry(product).State = EntityState.Modified;

        }

        await context.SaveChangesAsync();
    }

    public bool ExistsByCategoryId(Guid categoryId)
    {
        if (context.Products.Any(p => p.CategoryId == categoryId))
        {
            return true;
        }
        return false;
    }
}