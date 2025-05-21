using Service.BusinessValidators;
using Service.Contracts.Repository;
using Service.Entities;
using Service.Services.Interfaces;

namespace Service.Services.Implementation;

public class CategoryService(ICategoryRepository repository, CategoryBusinessValidator categoryBusinessValidator) : ICategoryService
{


    public async Task Add(Category category)
    {
        await categoryBusinessValidator.EnsureCodeIsUnique(category.Code);
        await categoryBusinessValidator.EnsureTitleIsUnique(category.Title);

        category.Id = Guid.NewGuid();
        await repository.Add(category);

        if (category.ParentCategoryId != null)
        {
            Category parent = await categoryBusinessValidator.EnsureIdExists(category.ParentCategoryId.Value);
            await repository.AddChildToParent(parent, category);
        }
    }

    public async Task Update(Guid id, Category dto)
    {
        Category existingCategory = await categoryBusinessValidator.EnsureIdExists(id);
        await categoryBusinessValidator.EnsureTitleIsUnique(dto.Title);
        categoryBusinessValidator.EnsureNotSettingItselfAsParent(existingCategory, dto);
        await categoryBusinessValidator.HandleParentCategoryChange(existingCategory, dto.ParentCategoryId);

        existingCategory.Title = dto.Title;
        existingCategory.Code = dto.Code;
        existingCategory.Description = dto.Description;
        existingCategory.ParentCategoryId = dto.ParentCategoryId;
        await repository.Update(existingCategory);
    }
    
    public async Task<Category> GetById(Guid id)
    {
        return await categoryBusinessValidator.EnsureIdExists(id);
    }

    public async Task<List<Category>> GetAll()
    {
        return await repository.GetAll();
    }

    public async Task<List<Category>> GetAllParents()
    {
        return await repository.GetAllParents();
    }
    
    public async Task SoftDelete(Guid id)
    {
        Category cat = await DeleteLogic(id);
        await repository.SoftDelete(cat.Id);
    }
    
    private async Task<Category> DeleteLogic(Guid id)
    {
        Category category = await categoryBusinessValidator.EnsureIdExists(id);
        categoryBusinessValidator.EnsureCategoryHasNoChildren(category);
        categoryBusinessValidator.EnsureCategoryHasNoProducts(category);
        if (category.ParentCategoryId != null)
        {
            Category parent = await categoryBusinessValidator.EnsureIdExists(category.ParentCategoryId.Value);
            await repository.DeleteChildFromParent(parent, category);
        }

        return category;
    }
}
