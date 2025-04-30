using Service.Contracts.Repository;
using Service.Entities;
using Service.Exceptions.CategoryExceptions;

namespace Service.Services.Implementation;

public class CategoryBusinessValidator(ICategoryRepository repository)
{
    public void EnsureCodeIsUnique(string code)
    {
        if (repository.GetCategoryByCode(code) != null)
        {
            throw new CategoryCodeIsNotUniqueException(code);
        }
    }

    public void EnsureTitleIsUnique(string title)
    {
        if (repository.GetCategoryByTitle(title) != null)
        {
            throw new CategoryTitleIsNotUniqueException(title);
        }
    }

    public void EnsureIdExists(Guid id)
    {
        if (repository.GetById(id) == null)
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
            Category oldParent = repository.GetById(category.ParentCategoryId.Value)!;
            oldParent.ChildCategories.Remove(category);
        }

        if (newParentId.HasValue)
        {
            Category newParent = repository.GetById(newParentId.Value)!;
            newParent.ChildCategories.Add(category);
        }

        category.ParentCategoryId = newParentId;
    }
}