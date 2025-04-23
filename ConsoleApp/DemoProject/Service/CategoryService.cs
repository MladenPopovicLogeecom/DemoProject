using WebApplication1.model;
using WebApplication1.model.entities;

namespace WebApplication1.Service;

public class CategoryService 
{
    private readonly Database Database;

    public CategoryService(Database database)
    {
        Database = database;
    }

    public void AddCategory(Category category)
    {
        if (Database.Categories.Any(c => c.Code == category.Code))
        {
            throw new Exception("Category with that code already exists");
        }
        if (Database.Categories.Any(c => c.Title == category.Code))
        {
            throw new Exception("Category with that name already exists");
        }
        
        if (category.ParentCategoryId != null)
        {
            Database.GetCategoryWithId(category.ParentCategoryId.Value).ChildCategories.Add(category); 
        }

        category.Id = Guid.NewGuid();
        Database.Categories.Add(category);
        
    }

    public void DeleteCategoryWithId(Guid id)
    {
        var category = Database.GetCategoryWithId(id);
        if (category == null)
        {
            throw new NotFoundException("There is no category with such an ID.");
        }

        //Category with child categories cannot be deleted
        if (category.ChildCategories.Count != 0)
        {
            throw new Exception("Category with child categories cannot be deleted");
        }

        Database.Categories.Remove(category);
        
    }

    public void UpdateCategory(Category updatedCategory)
    {
        var existingCategory = Database.GetCategoryWithId(updatedCategory.Id);
        if (existingCategory == null)
        {
            throw new NotFoundException("There is no category with such an ID.");
        }
        //Check if parent is different
        if (existingCategory.ParentCategoryId != updatedCategory.ParentCategoryId)
        {
            //If existingCategory had parent,remove
            if (existingCategory.ParentCategoryId != null)
            {
                Database.GetCategoryWithId(existingCategory.ParentCategoryId.Value).ChildCategories.Remove(existingCategory);
            }

            //If new parent is set,add
            if (updatedCategory.ParentCategoryId != null)
            {
                Database.GetCategoryWithId(updatedCategory.ParentCategoryId.Value).ChildCategories.Add(existingCategory);
            }
        }
        
        existingCategory.Title = updatedCategory.Title;
        existingCategory.Code = updatedCategory.Code;
        existingCategory.Description = updatedCategory.Description;
        existingCategory.ParentCategoryId = updatedCategory.ParentCategoryId;
        
    }

    public Category? GetCategoryWithId(Guid id)
    {
        Category cat = Database.GetCategoryWithId(id);
        if (cat == null)
        {
            throw new NotFoundException("There is no category with the given ID.");
        }
        return cat;
    }

    public List<Category> GetAllCategories()
    {
        return Database.Categories;
    }
}