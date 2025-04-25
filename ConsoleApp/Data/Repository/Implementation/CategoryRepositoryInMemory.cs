using Domain.Model.Entities;
using PresentationLayer.Repository.Interface;

namespace PresentationLayer.Repository.Implementation;

public class CategoryRepositoryInMemory : ICategoryRepository
{
    public List<Category> Categories { get; set; }

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

    
    public void EnsureCodeIsUnique(string code)
    {
        if (Categories.Any(c => c.Code == code))
        {
            throw new Exception("Category with that code already exists");
        }
    }

    public void EnsureTitleIsUnique(string title)
    {
        if (Categories.Any(c => c.Title == title))
        {
            throw new Exception("Category with that name already exists");
        }
    }

    public void EnsureIdExists(Guid id)
    {
        if (!Categories.Any(c => c.Id == id))
        {
            throw new Exception("Category with that ID does not exist");
        }
    }

    public void EnsureCategoryHasNoChildren(Category category)
    {
        if (category.ChildCategories != null && category.ChildCategories.Any())
        {
            throw new Exception("Cannot perform this operation. Category has child categories.");
        }
    }

    public void EnsureNotSettingItselfAsParent(Category category, Category dto)
    {
        if (dto.ParentCategoryId == category.Id)
        {
            throw new Exception("A category cannot be its own parent.");
        }
    }

    public void HandleParentCategoryChange(Category category, Guid? newParentId)
    {
        if (category.ParentCategoryId == newParentId)
        {
            return;
        }

        if (category.ParentCategoryId.HasValue)
        {
            var oldParent = GetCategoryById(category.ParentCategoryId.Value);
            oldParent.ChildCategories.Remove(category);
        }

        if (newParentId.HasValue)
        {
            var newParent = GetCategoryById(newParentId.Value);
            newParent.ChildCategories.Add(category);
        }

        category.ParentCategoryId = newParentId;
    }
}