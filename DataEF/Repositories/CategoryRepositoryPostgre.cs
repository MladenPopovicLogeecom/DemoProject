using DataEF.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Service.Contracts.Repository;
using Service.Entities;

namespace DataEF.Repositories;

public class CategoryRepositoryPostgre(ApplicationDbContext context) : ICategoryRepository
{
    public async Task Add(Category entity)
    {
        await context.Categories.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category != null)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }

    public async Task Update(Category entity)
    {
        context.Categories.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task<Category?> GetById(Guid id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task<List<Category>> GetAll()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<List<Category>> GetAllParents()
    {
        return await context.Categories.Where(c => c.ParentCategoryId == null).ToListAsync();
    }

    
    public async Task DeleteChildFromParent(Category parent, Category child)
    {
        var childCategory = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == child.Id && c.ParentCategoryId == parent.Id);

        if (childCategory != null)
        {
            childCategory.ParentCategoryId = null;
            await context.SaveChangesAsync();
        }
    }

    public async Task<Category?> GetCategoryByTitle(string title)
    {
        return await context.Categories
            .FirstOrDefaultAsync(c => c.Title == title);
    }

    public async Task<Category?> GetCategoryByCode(string code)
    {
        return await context.Categories
            .FirstOrDefaultAsync(c => c.Code == code);
    }
}