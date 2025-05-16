using System.Threading.Channels;
using DataEF.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Service.Contracts.Repository;
using Service.Entities;

namespace DataEF.Repositories;

public class CategoryRepositoryPostgre(ApplicationDbContext context, Channel<string> channel) : ICategoryRepository
{
    public async Task Add(Category entity)
    {
        await channel.Writer.WriteAsync("Adding category");
        context.Categories.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task HardDelete(Guid id)
    {
        await channel.Writer.WriteAsync("Deleting category");
        var category = await context.Categories.FindAsync(id);
        if (category != null)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Category>> GetAllDeletedBefore(DateTime date)
    {
        var threshold = date.AddMinutes(-5);
        return await context.Categories
            .Where(c => c.DeletedAt != null && c.DeletedAt < threshold)
            .ToListAsync();
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
            .FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);
    }


    public async Task<List<Category>> GetAll()
    {
        await channel.Writer.WriteAsync("Getting all categories");
        return await context.Categories
            .Where(c => c.DeletedAt == null)
            .Include(c => c.ChildCategories)
            .ToListAsync();
    }


    public async Task<List<Category>> GetAllParents()
    {
        await channel.Writer.WriteAsync("Getting all parent categories");
        return await context.Categories
            .Where(c => c.ParentCategoryId == null && c.DeletedAt == null)
            .Include(c => c.ChildCategories)
            .ToListAsync();
    }
    
    public async Task HardDeleteBeforeDate(DateTime date)
    {
        //var threshold = date.AddMinutes(-5);
        var threshold = date.AddSeconds(-15); //Test case

        var oldDeletedCategories = await context.Categories
            .Where(c => c.DeletedAt != null && c.DeletedAt < threshold)
            .ToListAsync();

        context.Categories.RemoveRange(oldDeletedCategories);
        await context.SaveChangesAsync();
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

    public async Task AddChildToParent(Category parent, Category child)
    {
        await channel.Writer.WriteAsync("Trying to add child to parent");

        var parentCategory = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == parent.Id && c.DeletedAt == null);

        var childCategory = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == child.Id && c.DeletedAt == null);

        //Checked in service, it will not be null;
        childCategory!.ParentCategoryId = parentCategory!.Id;
        await context.SaveChangesAsync();
        
    }


    public async Task SoftDelete(Guid id)
    {
        await channel.Writer.WriteAsync("Soft deleting category");
        Category category = (await context.Categories.FindAsync(id))!;
        category.DeletedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public Task HardDelete()
    {
        return Task.CompletedTask;
    }

    public async Task<Category?> GetCategoryByTitle(string title)
    {
        await channel.Writer.WriteAsync("Getting category by title");
        return await context.Categories
            .FirstOrDefaultAsync(c => c.Title == title && c.DeletedAt == null);
    }


    public async Task<Category?> GetCategoryByCode(string code)
    {
        await channel.Writer.WriteAsync("Getting category by Code");
        return await context.Categories
            .FirstOrDefaultAsync(c => c.Code == code && c.DeletedAt == null);
    }

}