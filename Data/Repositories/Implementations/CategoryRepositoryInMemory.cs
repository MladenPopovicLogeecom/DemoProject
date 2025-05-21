using Service.Contracts.Repository;
using Service.Entities;

namespace Data.Repositories.Implementations;

public class CategoryRepositoryInMemory : ICategoryRepository
{
    private readonly List<Category> categories = new();

    public Task Add(Category category)
    {
        categories.Add(category);

        return Task.CompletedTask;
    }

    public Task Update(Category category)
    {
        var index = categories.FindIndex(c => c.Id == category.Id);
        categories[index] = category;

        return Task.CompletedTask;
    }

    public Task<List<Category>> GetAllParents()
    {
        List<Category> parents = categories.Where(c => c.ParentCategoryId == null).ToList();

        return Task.FromResult(parents);
    }

    public Task HardDeleteBeforeDate(DateTime date, double threshold)
    {
        throw new NotImplementedException();
    }

    public Task SoftDelete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteChildFromParent(Category parent, Category child)
    {
        parent.ChildCategories.Remove(child);
        Update(parent);

        return Task.CompletedTask;
    }

    public Task AddChildToParent(Category parent, Category child)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetById(Guid id)
    {
        return Task.FromResult(categories.FirstOrDefault(c => c.Id == id));
    }

    public Task<Category?> GetCategoryByTitle(string title)
    {
        return Task.FromResult(categories.FirstOrDefault
            (c => c.Title.Equals(title, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<Category?> GetCategoryByCode(string code)
    {
        return Task.FromResult(categories.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<List<Category>> GetAll()
    {
        return Task.FromResult(categories);
    }
}