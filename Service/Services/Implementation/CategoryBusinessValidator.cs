using Service.Contracts.Repository;
using Service.Entities;
using Service.Exceptions.CategoryExceptions;

namespace Service.Services.Implementation;

public class CategoryBusinessValidator(ICategoryRepository repository)
{
    public void EnsureCodeIsUnique(string code)
    {
        if (repository.GetCategoryByCode(code).GetAwaiter().GetResult() != null)
        {
            throw new CategoryCodeIsNotUniqueException(code);
        }
    }

    public void EnsureTitleIsUnique(string title)
    {
        if (repository.GetCategoryByTitle(title).GetAwaiter().GetResult() != null)
        {
            throw new CategoryTitleIsNotUniqueException(title);
        }
    }

    public void EnsureIdExists(Guid id)
    {
        if (repository.GetById(id).GetAwaiter().GetResult() == null)
        {
            throw new CategoryWithIdNotFoundException(id);
        }
    }

    public void EnsureCategoryHasNoChildren(Category category)
    {
        if (category.ChildCategories != null && category.ChildCategories!.Any())
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
            Category oldParent = repository.GetById(category.ParentCategoryId.Value).GetAwaiter().GetResult()!;
            oldParent.ChildCategories.Remove(category);
        }

        if (newParentId.HasValue)
        {
            Category newParent = repository.GetById(newParentId.Value).GetAwaiter().GetResult()!;
            newParent.ChildCategories.Add(category);
        }

        category.ParentCategoryId = newParentId;
    }
}