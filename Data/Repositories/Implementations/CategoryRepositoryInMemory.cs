using Service.Contracts.Repository;
using Service.Entities;

namespace PresentationLayer.Repositories.Implementations;

public class CategoryRepositoryInMemory : ICategoryRepository
{
    private readonly List<Category> categories = new();

    public void Add(Category category)
    {
        categories.Add(category);
    }

    public void Delete(Guid id)
    {
        categories.Remove(GetById(id)!);
    }

    public void Update(Category category)
    {
        int index = categories.FindIndex(c => c.Id == category.Id);
        categories[index] = category;
    }

    public List<Category> GetAllParents()
    {
        return GetAll()
            .Where(c => c.ParentCategoryId == null)
            .ToList();
    }


    public void DeleteChildFromParent(Category parent, Category child)
    {
        parent.ChildCategories.Remove(child);
        Update(parent);
    }

    public Category? GetById(Guid id)
    {
        return categories.FirstOrDefault(c => c.Id == id);
    }

    public Category? GetCategoryByTitle(string title)
    {
        return categories.FirstOrDefault(c => c.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public Category? GetCategoryByCode(string code)
    {
        return categories.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
    }

    public List<Category> GetAll()
    {
        return categories;
    }
}