using DataEF.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Service.BackgroundServices;
using Service.Contracts.Repository;
using Service.Entities;

namespace DataEF.Repositories;

public class CategoryRepositoryPostgre(ApplicationDbContext context, MessageChannel messageChannel)
    : ICategoryRepository
{
    public async Task Add(Category entity)
    {
        messageChannel.AddMessage("Adding category");
        context.Categories.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Category entity)
    {
        messageChannel.AddMessage("Updating category");
        context.Categories.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task<Category?> GetById(Guid id)
    {
        messageChannel.AddMessage("Getting category by id");

        return await context.Categories
            .Include(c => c.ChildCategories)
            .FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);
    }

    public async Task<List<Category>> GetAll()
    {
        messageChannel.AddMessage("Getting all categories");

        return await context.Categories
            .Where(c => c.DeletedAt == null)
            .Include(c => c.ChildCategories)
            .ToListAsync();
    }

    public async Task<List<Category>> GetAllParents()
    {
        messageChannel.AddMessage("Getting all parent categories");

        return await context.Categories
            .Where(c => c.ParentCategoryId == null && c.DeletedAt == null)
            .Include(c => c.ChildCategories)
            .ToListAsync();
    }

    public async Task HardDeleteBeforeDate(DateTime date, double threshold)
    {
        DateTime threshold2 = date.AddMinutes(-threshold);

        List<Category>? oldDeletedCategories = await context.Categories
            .Where(c => c.DeletedAt != null && c.DeletedAt < threshold2)
            .ToListAsync();

        context.Categories.RemoveRange(oldDeletedCategories);
        await context.SaveChangesAsync();
    }

    public async Task DeleteChildFromParent(Category parent, Category child)
    {
        messageChannel.AddMessage("Trying to delete child from parent");
        Category? childCategory = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == child.Id && c.ParentCategoryId == parent.Id);

        if (childCategory != null)
        {
            childCategory.ParentCategoryId = null;
            await context.SaveChangesAsync();
        }
    }

    public async Task AddChildToParent(Category parent, Category child)
    {
        messageChannel.AddMessage("Trying to add child to parent");

        Category? parentCategory = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == parent.Id && c.DeletedAt == null);

        Category? childCategory = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == child.Id && c.DeletedAt == null);

        //Checked in service, it will not be null;
        childCategory!.ParentCategoryId = parentCategory!.Id;
        await context.SaveChangesAsync();
    }

    public async Task SoftDelete(Guid id)
    {
        messageChannel.AddMessage("Soft deleting category");
        Category? category = (await context.Categories.FindAsync(id))!;
        category.DeletedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public async Task<Category?> GetCategoryByTitle(string title)
    {
        messageChannel.AddMessage("Getting category by title");

        return await context.Categories
            .FirstOrDefaultAsync(c => c.Title == title && c.DeletedAt == null);
    }

    public async Task<Category?> GetCategoryByCode(string code)
    {
        messageChannel.AddMessage("Getting category by title");

        return await context.Categories
            .FirstOrDefaultAsync(c => c.Code == code && c.DeletedAt == null);
    }
}