using Service.Contracts.Repository;
using Service.Entities;
using Service.Exceptions.CategoryExceptions;

namespace Service.BusinessValidators;

public class CategoryBusinessValidator(ICategoryRepository categoryRepository, IProductRepository productRepository)
{
    public async Task EnsureCodeIsUnique(string code)
    {
        if (await categoryRepository.GetCategoryByCode(code) != null)
        {
            throw new CategoryCodeIsNotUniqueException(code);
        }
    }

    public async Task EnsureTitleIsUnique(string title)
    {
        if (await categoryRepository.GetCategoryByTitle(title) != null)
        {
            throw new CategoryTitleIsNotUniqueException(title);
        }
    }

    public async Task<Category> EnsureCategoryExists(Guid id)
    {
        var category = await categoryRepository.GetById(id);
        if (category == null)
        {
            throw new CategoryWithIdNotFoundException(id);
        }

        return category;
    }

    public void EnsureCategoryHasNoChildren(Category category)
    {
        if (category.ChildCategories != null && category.ChildCategories.Any())
        {
            throw new CategoryHasChildrenException(category.Title);
        }
    }

    public void EnsureCategoryHasNoProducts(Category category)
    {
        if (productRepository.ExistsByCategoryId(category.Id))
        {
            throw new CategoryHasProductsException(category.Title);
        }
    }

    public void EnsureNotSettingItselfAsParent(Category category, Category dto)
    {
        if (dto.ParentCategoryId == category.Id)
        {
            throw new Exception("A category cannot be its own parent.");
        }
    }

    public async Task HandleParentCategoryChange(Category category, Guid? newParentId)
    {
        if (category.ParentCategoryId == newParentId)
        {
            return;
        }

        if (category.ParentCategoryId.HasValue)
        {
            var oldParent = (await categoryRepository.GetById(category.ParentCategoryId.Value))!;
            oldParent.ChildCategories.Remove(category);
        }

        if (newParentId.HasValue)
        {
            var newParent = (await categoryRepository.GetById(newParentId.Value))!;
            newParent.ChildCategories.Add(category);
        }

        category.ParentCategoryId = newParentId;
    }
}