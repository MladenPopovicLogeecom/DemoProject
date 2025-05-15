using System.Threading.Channels;
using DataEF.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Service.BackgroundServices;
using Service.Contracts.Repository;
using Service.Entities;

namespace DataEF.Repositories;

public class CategoryRepositoryPostgre(ApplicationDbContext context,Channel<string>channel) : ICategoryRepository
{
    public async Task Add(Category entity)
    {
        await channel.Writer.WriteAsync("Adding category");
        await context.Categories.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await channel.Writer.WriteAsync("Deleting category");
        var category = await context.Categories.FindAsync(id);
        if (category != null)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
    
    public async Task Update(Category entity)
    {
        await channel.Writer.WriteAsync("Updating category");
        context.Categories.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task<Category?> GetById(Guid id)
    {
        await channel.Writer.WriteAsync("Getting category by id");
        return await context.Categories
            .Include(c => c.ChildCategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> GetAll()
    {
        await channel.Writer.WriteAsync("Getting all categories");
        return await context.Categories.Include(c => c.ChildCategories)
            .ToListAsync();
    }

    public async Task<List<Category>> GetAllParents()
    {
        await channel.Writer.WriteAsync("Getting all parent categories");
        return await context.Categories.Where(c => c.ParentCategoryId == null)
            .Include(c => c.ChildCategories).ToListAsync();
    }
    
    public async Task DeleteChildFromParent(Category parent, Category child)
    {
        await channel.Writer.WriteAsync("Trying to delete child from parent");
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
        await channel.Writer.WriteAsync("Getting category by title");
        return await context.Categories
            .FirstOrDefaultAsync(c => c.Title == title);
    }

    public async Task<Category?> GetCategoryByCode(string code)
    {
        await channel.Writer.WriteAsync("Getting category by Code");
        return await context.Categories
            .FirstOrDefaultAsync(c => c.Code == code);
    }
}