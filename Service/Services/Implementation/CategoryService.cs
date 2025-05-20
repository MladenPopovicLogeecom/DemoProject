using Service.BusinessValidators;
using Service.Contracts.Repository;
using Service.Entities;
using Service.Services.Interfaces;

namespace Service.Services.Implementation;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository repository;
    private readonly CategoryBusinessValidator categoryBusinessValidator;

    public CategoryService(ICategoryRepository iCategoryRepository)
    {
        repository = iCategoryRepository;
        categoryBusinessValidator = new CategoryBusinessValidator(repository);
    }

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
        await repository.SoftDelete(cat.Id!.Value);
    }
    
    private async Task<Category> DeleteLogic(Guid id)
    {
        Category category = await categoryBusinessValidator.EnsureIdExists(id);
        categoryBusinessValidator.EnsureCategoryHasNoChildren(category);
        if (category.ParentCategoryId != null)
        {
            Category parent = await categoryBusinessValidator.EnsureIdExists(category.ParentCategoryId.Value);
            await repository.DeleteChildFromParent(parent, category);
        }

        return category;
    }

    public Task SeedDatabase()
    {
        for (var i = 0; i < 3; i++)
        {
            Category cat = new Category("Title " + i, "Code " + i,
                "Description " + i,
                null, null)
            {
                Id = Guid.Empty
            };

            repository.Add(cat);
        }

        return Task.CompletedTask;
    }
}