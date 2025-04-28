using Domain.Model.Entities;
using PresentationLayer.Repository.Implementation;
using Service.MyExceptions;
using Service.MyExceptions.CategoryExceptions;

namespace Service.Service.Implementation;

public class CategoryValidator
{
    private readonly CategoryRepositoryInMemory repository;

    public CategoryValidator(CategoryRepositoryInMemory repository)
    {
        this.repository = repository;
    }
        
    public void EnsureCodeIsUnique(string code)
    {
        if (repository.GetAllCategories().Any(c => c.Code == code))
        {
            throw new CategoryCodeIsNotUniqueException(code);
        }
    }

    public void EnsureTitleIsUnique(string title)
    {
        if (repository.GetAllCategories().Any(c => c.Title == title))
        {
            throw new CategoryTitleIsNotUniqueException(title);
        }
    }

    public void EnsureIdExists(Guid id)
    {
        if (!repository.GetAllCategories().Any(c => c.Id == id))
        {
            throw new CategoryWithIdNotFoundException(id);
        }
    }

    public void EnsureCategoryHasNoChildren(Category category)
    {
        if (category.ChildCategories != null && category.ChildCategories.Any())
        {
            throw new CategoryHasChildrenException(category.Title);
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
            var oldParent = repository.GetCategoryById(category.ParentCategoryId.Value);
            oldParent.ChildCategories.Remove(category);
        }

        if (newParentId.HasValue)
        {
            var newParent = repository.GetCategoryById(newParentId.Value);
            newParent.ChildCategories.Add(category);
        }

        category.ParentCategoryId = newParentId;
    }
}