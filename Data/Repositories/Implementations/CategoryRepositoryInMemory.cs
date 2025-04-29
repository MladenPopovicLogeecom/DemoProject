using PresentationLayer.Entities;

namespace PresentationLayer.Repositories.Implementations;

public class CategoryRepositoryInMemory
{
    private List<Category> Categories = new();

    public void AddCategory(Category category)
    {
        Categories.Add(category);
    }

    public void DeleteCategory(Category category)
    {
        Categories.Remove(category);
    }

    private void SaveCategory(Category category)
    {
        int index = Categories.FindIndex(c => c.Id == category.Id);
        Categories[index] = category;
    }

    public void DeleteChildFromParent(Category parent, Category child)
    {
        parent.ChildCategories.Remove(child);
        SaveCategory(parent);
    }

    public Category? GetCategoryById(Guid id)
    {
        return Categories.FirstOrDefault(c => c.Id == id);
    }

    public Category? GetCategoryByTitle(string title)
    {
        return Categories.FirstOrDefault(c => c.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public Category? GetCategoryByCode(string code)
    {
        return Categories.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
    }

    public List<Category> GetAllCategories()
    {
        return Categories;
    }
}