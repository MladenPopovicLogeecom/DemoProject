using DemoProject.model;
using DemoProject.model.entities;

namespace DemoProject.Service;

public class CategoryService
{
    private readonly Database Database;

    public CategoryService(Database database)
    {
        Database = database;
    }

    public void AddCategory(Category category)
    {
        EnsureCodeIsUnique(category.Code);
        EnsureTitleIsUnique(category.Title);

        if (category.ParentCategoryId != null)
        {
            EnsureIdExists(category.ParentCategoryId.Value);
            Database.GetCategoryWithId(category.ParentCategoryId.Value)!.ChildCategories.Add(category);
        }

        category.Id = Guid.NewGuid();
        Database.Categories.Add(category);
    }


    public void DeleteCategoryWithId(Guid id)
    {
        EnsureIdExists(id);
        var category = Database.GetCategoryWithId(id);
        EnsureCategoryHasNoChildren(category!);
        Database.Categories.Remove(category!);
    }

    public void UpdateCategory(Guid id, Category dto)
    {
        EnsureIdExists(id);
        EnsureTitleIsUnique(dto.Title);
        var existingCategory = Database.GetCategoryWithId(id)!;
        EnsureNotSettingItselfAsParent(existingCategory, dto);
        HandleParentCategoryChange(existingCategory, dto.ParentCategoryId);

        existingCategory.Title = dto.Title;
        existingCategory.Code = dto.Code;
        existingCategory.Description = dto.Description;
        existingCategory.ParentCategoryId = dto.ParentCategoryId;
    }

    public Category GetCategoryWithId(Guid id)
    {
        EnsureIdExists(id);
        return Database.GetCategoryWithId(id);
    }

    public List<Category> GetAllCategories()
    {
        return Database.Categories;
    }
    
    private void EnsureCodeIsUnique(string code)
    {
        if (Database.Categories.Any(c => c.Code == code))
        {
            throw new Exception("Category with that code already exists");
        }
    }

    private void EnsureTitleIsUnique(string title)
    {
        if (Database.Categories.Any(c => c.Title == title))
        {
            throw new Exception("Category with that name already exists");
        }
    }

    private void EnsureIdExists(Guid id)
    {
        if (!Database.Categories.Any(c => c.Id == id))
        {
            throw new Exception("Category with that ID does not exist");
        }
    }

    private void EnsureCategoryHasNoChildren(Category category)
    {
        if (category.ChildCategories != null && category.ChildCategories.Any())
        {
            throw new Exception("Cannot perform this operation. Category has child categories.");
        }
    }

    private void EnsureNotSettingItselfAsParent(Category category, Category dto)
    {
        if (dto.ParentCategoryId == category.Id)
        {
            throw new Exception("A category cannot be its own parent.");
        }
    }

    private void HandleParentCategoryChange(Category category, Guid? newParentId)
    {
        if (category.ParentCategoryId == newParentId)
        {
            return;
        }

        if (category.ParentCategoryId.HasValue)
        {
            var oldParent = Database.GetCategoryWithId(category.ParentCategoryId.Value)!;
            oldParent.ChildCategories.Remove(category);
        }

        if (newParentId.HasValue)
        {
            var newParent = Database.GetCategoryWithId(newParentId.Value)!;
            newParent.ChildCategories.Add(category);
        }

        category.ParentCategoryId = newParentId;
    }
}