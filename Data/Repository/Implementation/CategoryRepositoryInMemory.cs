using Domain.Model.Entities;

namespace PresentationLayer.Repository.Implementation;

public class CategoryRepositoryInMemory 
{
    private List<Category> Categories { get; set; } = new();

    public CategoryRepositoryInMemory()
    {
        Categories = new List<Category>();
    }

    public void AddCategory(Category category)
    {
        Categories.Add(category);
    }
    
    public void DeleteCategory(Category category)
    {
        Categories.Remove(category);
    }
    
    public void SaveCategory(Category category)
    {
        var index = Categories.FindIndex(c => c.Id == category.Id);
        Categories[index] = category;
    }

    public void DeleteChildFromParent(Category parent, Category child)
    {
        parent.ChildCategories.Remove(child);
        SaveCategory(parent);
    }
    
    public Category GetCategoryById(Guid id)
    {
        //It will never return null, because validations are done before
        //but who knows
        return Categories.FirstOrDefault(c => c.Id == id);

    }
    public List<Category> GetAllCategories()
    {
        return Categories;
    }


}